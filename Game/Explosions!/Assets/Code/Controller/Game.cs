using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Assets.Code.Components;
using Assets.Code.Model;

namespace Assets.Code.Controller 
{
    /// <summary>
    /// Game Service
    /// </summary>
    public class Game : MonoBehaviour
    {
        /// <summary>
        /// This is a service that provides communication with a "server" (Whether it's a real server or not)
        /// It can be changed during run-time
        /// Options: ProductionServer, TestServer, DemoServer, ServerStub
        /// </summary>
        private Server server;
        private ThreadSafeActionQueue queue;
        //Cache
        //TODO: We are using threading, we should probably include locks
		private IEnumerable<Character> characters;
		private IEnumerable<Mission> missions;

        private IEnumerable<Recipe> purchaseItems;
        private IEnumerable<Recipe> craftItems;
        private int mainThread;

        //Game class is a singleton.
        public static Game Instance { get; private set; }

		public string username;
		public Character character; //TODO: Keep track of selected character 
		public bool CharacterSelected { get { return this.character != null; } }


        //Character prefab is initialized on Awake().
        //>"CharacterLoader" instantiated in "LoadCharacter()".
        private static GameObject characterPrefab;
        private static CharacterLoader characterInst;

        //Item prefab loading and initialization?
		private static Dictionary<string, GameObject> itemPrefabs;
		private static Dictionary<string, Item> itemComponents;

        #region Asynchronous Properties
        /****************************************************************************
         *  These properties will start a request for data and immediately return nothing
         *  But eventually they will return data
         *  If empty enumerable is returned, assume data is loading
        ****************************************************************************/

		public IEnumerable<Character> Characters
		{
			get
			{
				if (characters == null)
				{
					characters = Enumerable.Empty<Character>(); 	//Empty
					server.GetCharacters(username, (x) => characters = x); 		//TODO: lock?
				}
				return characters;
			}
		}

        public IEnumerable<Recipe> PurchasableItems
        {
            get
            {
                if (purchaseItems == null)
                {
                    purchaseItems = Enumerable.Empty<Recipe>(); //Empty
                    //TODO: lock?
                    server.GetPurchasableItems((x) => purchaseItems = x);
                }
                return purchaseItems;
            }
        }

        public IEnumerable<Mission> Missions
        {
            get
            {
                if (missions == null)
                {
                    missions = Enumerable.Empty<Mission>();
                    server.GetMissions(character, (x) => missions = x);
                }
                return missions;
            }
        } 

		public Inventory Inventory
		{
			get 
			{
				//TODO: Load Inventory ?
				return null;
			}
		}
        #endregion
        
        //TODO: Maybe combine these into 1 with a param?
        public void ShowCharacter()
        {
            if (characterInst != null)
            {
                characterInst.Show();
            }
        }

        public void HideCharacter()
        {
            if (characterInst != null)
            {
                characterInst.Hide();
            }
        }

        public void SetCharacter(Character character)
        {
            this.character = character;
        }

		#region Game Loading
        /// <summary>
        /// Safely loads character regardless of current thread
        /// </summary>
		public void LoadCharacter()
		{
            if (!OnMainThread)
            {
                InvokeOnMainThread(() => LoadCharacter());
                return;
            }
            if (character == null)
            {
                Debug.LogError("Cannot Load a Character before one is selected");
                return;
            }
			
			characterInst = Instantiate(characterPrefab.GetComponent<CharacterLoader>()) as CharacterLoader;
            DontDestroyOnLoad(characterInst);
            LoadInventory ();
		}

		private void LoadInventory()
		{
			server.GetInventory (character, (inventoryLoaded) => 
			{
				LoadItemsIntoInventory (inventoryLoaded);
			});
		}

		private void LoadEquipment()
		{
			server.GetEquipment (character, (equipment) => 
			{
				LoadItemsIntoEquipment (equipment);
			});
		}
	
		private void LoadItemsIntoInventory(IEnumerable<KeyValuePair<string, int>> toLoad)
		{
            if (!OnMainThread)
            {
				InvokeOnMainThread (() => LoadItemsIntoInventory(toLoad));
                return;
            }
			if (toLoad == null)
			{
				Debug.Log ("LoadItemsIntoInventory: Bad input.");
				return;
			}
			
			Inventory inventory = characterInst.GetComponent<Inventory>();
			
			if (inventory == null)
			{
				Debug.Log ("LoadItemsIntoInventory: Cannot load Inventory.");
				return;
			}
			
			foreach (KeyValuePair<string, int> entry in toLoad)
			{
				string itemName = entry.Key;
				int itemQuantity = entry.Value;
				Item itemComp;
				if (itemComponents.TryGetValue(itemName, out itemComp))
				{
					inventory.Add (itemComp, entry.Value);
				}
				else
				{
					Debug.Log ("Item Component [" + itemName +"] could not be found.");
				}
			}
			
			Debug.Log ("Inventory Loaded: ");
			inventory.Print ();
			
			LoadEquipment ();
		}
		
		private void LoadItemsIntoEquipment(IEnumerable<KeyValuePair<string, Slot>> toLoad)
		{
            if (!OnMainThread)
            {
                InvokeOnMainThread(() => LoadItemsIntoEquipment(toLoad));
                return;
            }
			if (toLoad == null) 
			{
				Debug.Log ("LoadItemsIntoEquipment: Bad input.");
				return;
			}
			
			EquipmentManager equipManager = characterInst.GetComponent<EquipmentManager>();
			Inventory equipInventory = characterInst.GetComponent<Inventory>();
			
			if (equipManager == null || equipInventory == null)
			{
				Debug.Log ("LoadItemsIntoEquipment: Cannot load Equipment/Inventory.");
				return;
			}
			
			foreach (KeyValuePair<string, Slot> entry in toLoad)
			{
				string itemName = entry.Key;
				Slot itemSlot = entry.Value;
				Item itemComp;
				if (itemComponents.TryGetValue(itemName, out itemComp))
				{
					equipInventory.Add (itemComp);
					equipManager.Equip (itemComp, itemSlot);
				}
				else
				{
					Debug.Log ("Item Component ["+itemName+"] could not be found.");
				}
			}
			
			Debug.Log ("Equipment Loaded -- Printing Inventory & Equipment");
			equipInventory.Print ();
			equipManager.Print ();
		}
		#endregion
		
		#region Server Interface
		public void Authenticate(string username, string password, Action<bool> asyncReturn)
        {
            server.Login(username, password, (validPlayer) => asyncReturn(validPlayer));
        }

        //Only game should speak to server directly
        public void IsServerOnline(Action<bool> asyncReturn)
        {
            server.IsAlive((alive) => asyncReturn(alive));
        }

        /// <summary>
        /// Returns whether or not the recipe was successful
        /// </summary>
        /// <param name="id">The recipe id we want to use</param>
        /// <returns></returns>
        public bool UseRecipe(Recipe recipe, Character inChar, Character outChar)
        {
            bool success = false;
            //TODO: Perform game-side verification of recipes (In here, or in a "Shop" class)
            //TODO: Store recipe until verified
            //TODO: Perform game-side execution of recipes

            //Assume this is successeful
            server.UseRecipe(recipe, inChar, outChar, (ok) =>
                {
                    //if (ok)
                    //{
                    //    //TODO: Remove recipe from undo list
                    //}
                    //else
                    //{
                    //    //TODO: Undo recipe and notify user of error?
                    //}
                });
            return success;
        } 
        #endregion

        #region Main Thread Invoking
        public bool OnMainThread { 
            get 
            {
				return 	Thread.CurrentThread.ManagedThreadId == mainThread &&
						Thread.CurrentThread.GetApartmentState() == ApartmentState.Unknown && //Unity main thread = 'Unknown'
						!Thread.CurrentThread.IsBackground &&
						!Thread.CurrentThread.IsThreadPoolThread;
            } 
        }

        public void InvokeOnMainThread(Action task)
        {
            queue.InvokeOnMainThread(task);
        }

        public void LoadLevel(string name)
        {
            if (!OnMainThread)
            {
                InvokeOnMainThread(() => LoadLevel(name));
            }
			else
			{
                Application.LoadLevel(name);
			}
        }
        #endregion

        #region Initialization
        public void Awake()
        {
            //TODO: Perform dependency injection through inspector editing
            //TODO: Support unit test depency injection

            this.server = new Server(Server.PRODUCTION_URL);
            this.username = null;
            this.character = null;
            this.purchaseItems = null;
            this.craftItems = null;
            this.mainThread = Thread.CurrentThread.ManagedThreadId;
            this.queue = new ThreadSafeActionQueue();
            DontDestroyOnLoad(this);

			LoadGameResources ();
            Init(this); //Assign this instance to singleton
        }

        public void LoadGameResources()
        {
            characterPrefab = Resources.Load<GameObject>("Prefabs/Character");

            itemPrefabs = new Dictionary<string, GameObject>();
            itemPrefabs.Add("Laser Weapon", Resources.Load<GameObject>("Prefabs/Items/Laser Weapon"));
            itemPrefabs.Add("Health Booster", Resources.Load<GameObject>("Prefabs/Items/Health Booster"));
            itemPrefabs.Add("Gold", Resources.Load<GameObject>("Prefabs/Items/Gold"));

            itemComponents = new Dictionary<string, Item>();
            foreach (GameObject go in itemPrefabs.Values)
            {
                Item item = go.GetComponent<Item>();
                if (item != null)
                {
                    itemComponents.Add(item.Name, item);
                }
            }

        }

        public void Update()
        {
            queue.Update();
        }

        //Should only initialize Game on main thread
        //public Game(Server server)
        //{
        //    this.server = server;
        //    this.username = null;
        //    this.character = null;
        //    this.purchaseItems = null;
        //    this.craftItems = null;
        //    this.mainThread = Thread.CurrentThread.ManagedThreadId;
        //    CreateQueue();
        //}

        public void OnApplicationQuit()
        {
            //TODO: Clean up
            //TODO: Would we need this?
            //DestroyImmediate(this);
        }

        /// <summary>
        /// One of the first things to be called on startup
        /// </summary>
        public static void Init()
        {
            if (Instance == null)
            {
                GameObject gameGo = new GameObject();
                gameGo.name = "Game";
				gameGo.AddComponent<Game>();
            }
            //Instance = new Game(new Server(Server.PRODUCTION_URL));
        }

        /// <summary>
        /// Support for full game dependency injection
        /// </summary>
        /// <param name="customInstance"></param>
        public static void Init(Game customInstance)
        {
			Instance = customInstance;
        }

        /// <summary>
        /// Support for server dependency injection
        /// </summary>
        /// <param name="customServer"></param>
        public static void Init(Server customServer)
        {
            Init();
            Instance.ChangeServer(customServer);
        }

        private void ChangeServer(Server customServer)
        {
            this.server = customServer;
        } 
        #endregion
    }

}
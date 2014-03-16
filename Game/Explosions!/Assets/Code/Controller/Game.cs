﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Code.Components;
using Assets.Code.Model;

namespace Assets.Code.Controller 
{
    /// <summary>
    /// Game Service
    /// </summary>
    public class Game
    {
        /// <summary>
        /// This is a service that provides communication with a "server" (Whether it's a real server or not)
        /// It can be changed during run-time
        /// Options: ProductionServer, TestServer, DemoServer, ServerStub
        /// </summary>
        private Server server;

        private EquipmentManager equipManager; //

        //Cache
        //TODO: We are using threading, we should probably include locks
		private IEnumerable<Character> characters;
		private IEnumerable<Mission> missions;
        private IEnumerable<Recipe> purchaseItems;
        private IEnumerable<Recipe> craftItems;
		private IEnumerable<Item> inventory;

        public static Game Instance { get; private set; }

		public string username;
		public Character character; //TODO: Keep track of selected character 
		public bool CharacterSelected { get { return this.character != null; } }

        #region Asynchronous Properties
        /****************************************************************************
         *  These properties will start a request for data and immediately return nothing
         *  But eventually they will return data
         *  If empty enumerable is returned, assume data is loading
        ****************************************************************************/

	    public void Authenticate(string username, string password, Action<bool> asyncReturn)
	    {
		    server.Login(username, password, (validPlayer) => asyncReturn(validPlayer) );
	    }

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

		public IEnumerable<Inventory> Inventory
		{
			get 
			{
				return null;
			}
		}

        #endregion

        public Game(Server server)
        {
            this.server = server;
			this.username = null;
            this.character = null;
            this.purchaseItems = null;
            this.craftItems = null;
        }

        //Only game should speak to server directly
        public void IsServerOnline(Action<bool> asyncReturn)
        {
            server.IsAlive((alive) => asyncReturn(alive));
        }

		public void TestGetInventory(Action<IEnumerable<KeyValuePair<string, int>>> asyncReturn)
		{
			/** "inventory" is the async-returned List of item names/quantities; Game needs to initialize them. **/
			server.GetInventory(new Character(4, "TEST", 7, -8), (inventory) => asyncReturn(inventory));
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

        #region Initialization
        /// <summary>
        /// One of the first things to be called on startup
        /// </summary>
        public static void Init()
        {
			Instance = new Game(new Server(Server.PRODUCTION_URL));
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
            Instance = new Game(customServer);
        } 
        #endregion

    }

}
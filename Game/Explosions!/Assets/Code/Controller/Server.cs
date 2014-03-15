using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Net;
using SimpleJSON;
using Assets.Code.Model;
using Assets.Code.Components;

namespace Assets.Code.Controller
{
    /// <summary>
    /// Only this service should know how to communicate with the server
    /// Only this service should know the mapping between Objects and Database IDs
    /// </summary>
    public class Server 
    {
        public const string PRODUCTION_URL = "http://54.200.201.50";
        public const string XEFIER_URL = "http://54.213.248.49/";
	    public const string BRAHMDEEP_URL = "http://50.112.181.140";
	    public const string LOCAL_HOST = "localhost";
        public const string SERVER_PORT = ":80";
    
	    private const string IS_ALIVE_PATH = "isAlive";
	    private const string LOGIN_REQUEST_PATH = "loginRequest";
		private const string CHARACTERS_PATH = "character/getAll";
		private const string MISSIONS_PATH = "mission/getAll";
		private const string PURCHASABLES_PATH = "getPurchasables";
        private const string ADD_COOKIE_PATH = "addCookie";
        private const string HAS_COOKIE_PATH = "hasCookie";

	    public const int DEFAULT_TIMEOUT = 10000;

        //TODO: Thread pool?
        //private ThreadPool threads;
        private String url;
        private CookieContainer Cookies;
        private Dictionary<Character, int> characterIDs;
        private Dictionary<Recipe, int> recipeIDs;


        public Server(String url) 
        {
            this.url = url;
            this.Cookies = new CookieContainer();
            this.characterIDs = new Dictionary<Character, int>();
            //characterIDs.Add(Character.SHOP, -1);
            this.recipeIDs = new Dictionary<Recipe, int>();
        }

        /// <summary>
        /// Asynchronous Send
        /// </summary>
        /// <param name="path"></param>
        /// <param name="json"></param>
        /// <param name="asyncReturn"></param>
	    protected virtual void AsyncSend(string path, JSONClass json, Action<JSONNode> asyncReturn)
        {
            //TODO: Use a threadpool instead of creating a new thread every time
            //TODO: Terminate all pending threads on application exit!
            new Thread(() =>
            {
                asyncReturn(Send(path, json));
            }).Start();
        }

        /// <summary>
        /// Synchronous Send (Will block caller)
        /// </summary>
        /// <param name="path"></param>
        /// <param name="json"></param>
        /// <returns></returns>
	    protected virtual JSONNode Send(String path, JSONClass json) 
        {
            var location = this.url + SERVER_PORT + "/" + path;
            var uri = new Uri(location); // No need to build this every time
            var client = new CookieAwareWebClient(this.Cookies);
            client.Headers["Content-Type"] = "application/json";
            string response = client.UploadString(uri, json.ToString());

			Debug.Log ("sending... " + path + " " + json.ToString());
			Debug.Log ("" + response);
		    return JSON.Parse(response);
        }
	
	    // asyncReturn - boolean indicating whether or not the login was successful
	    public virtual void Login(String username, String password, Action<bool> asyncReturn){
		    JSONClass playerCredentials = new JSONClass();
		    playerCredentials.Add("user", username);
		    playerCredentials.Add("password", password);
		
		    //	protected virtual void AsyncSend(string path, JSONClass json, Action<JSONNode> asyncReturn)
		    AsyncSend (LOGIN_REQUEST_PATH, playerCredentials, (j) =>
		               {
			    asyncReturn (j ["result"].Value == "true");
		    });
	    }

		public virtual void GetCharacters(string username, Action<IEnumerable<Character>> asyncReturn)
		{
			JSONClass characters = new JSONClass ();
			characters.Add("user", username);

			Debug.Log ("---------> ASYNC SEND -- getAll --------------");
			AsyncSend (CHARACTERS_PATH, characters, (j) =>
				{
				//TODO: verify response contains all characters, parse into Character Objects, return list
				List<Character> ownedCharacters = new List<Character>();
				Debug.Log(j["characters"].Count);
				for (int currCharacter = 0; currCharacter < j["characters"].Count; currCharacter++) {
					ownedCharacters.Add (new Character(
						j["characters"].AsArray[currCharacter][2].ToString(),
						j["characters"].AsArray[currCharacter][3].AsInt,
						j["characters"].AsArray[currCharacter][4].AsInt));
				}

//				foreach (Character character in ownedCharacters){
//					Debug.Log(character.ToString());
//				}
					asyncReturn (ownedCharacters);
				});
		}

        public virtual void IsAlive(Action<bool> asyncReturn)
        {
		    //Empty JSON Request
            AsyncSend(IS_ALIVE_PATH, new JSONClass(), (j) =>
                {
                    asyncReturn(j["result"].Value == "1");
                });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="recipe"></param>
        /// <param name="inChar">Loses in items according to</param>
        /// <param name="outChar"></param>
        /// <returns></returns>
        public virtual void UseRecipe(Recipe recipe, Character inChar, Character outChar, Action<bool> asyncReturn)
        {
            bool success = false;
            //TODO: Retrieve id from recipeIDs (If we don't know about it, then it's not valid)
            //TODO: Call useRecipe on server
            asyncReturn(success);
        }

        public virtual void GetPurchasableItems(Action<IEnumerable<Recipe>> asyncReturn)
        {
			JSONClass purchasables = new JSONClass ();
			
			Debug.Log ("---------> ASYNC SEND -- get purchasables --------------");
			new Thread(() =>
			           {
				Send (PURCHASABLES_PATH, purchasables /*(j) =>
			           {				
				List<Recipe> itemPurchasables = new List<Recipe>();
				Debug.Log("Something is here");
				Debug.Log(j["purchasables"].Count);
				for (int currRecipe = 0; currRecipe < j["purchasables"].Count; currRecipe++) {
					itemPurchasables.Add (new Recipe(
						j["purchasables"].AsArray[currRecipe][0].AsInt,
						j["purchasables"].AsArray[currRecipe][1].AsInt,
						j["purchasables"].AsArray[currRecipe][2].ToString()));
				}

				asyncReturn (itemPurchasables);

			}*/
				      );
			}).Start();

            //TODO: Query server for this data
            //TODO: Store these recipes in the recipeIDs dictionary
            //throw new NotImplementedException();
        }

        public virtual void GetCraftableItems(Action<IEnumerable<Recipe>> asyncReturn)
        {
            //TODO: Query server for this data
            //TODO: Store these recipes in the recipeIDs dictionary
            throw new NotImplementedException();
        }

	    public virtual void GetMissions(Character character, Action<IEnumerable<Mission>> asyncReturn)
	    {
			JSONClass missions = new JSONClass ();

			AsyncSend (MISSIONS_PATH, missions, (j) =>
			    {
					List<Mission> missionsList = new List<Mission>();
					
					for (int currMission = 0; currMission < j["missions"].Count; currMission++) {
						missionsList.Add (new Mission(j["missions"].AsArray[currMission][0].AsInt));
					}
					asyncReturn (missionsList);
				});
	    }

        public virtual void GetInventory(Character character, Action<IEnumerable<Item>> asyncReturn)
        {
            throw new NotImplementedException();
        }

        public virtual void GetEquipment(Character character, Action<IEnumerable<Item>> asyncReturn)
        {
            //TODO: Order by slot id? (Otherwise we need to return Item/Slot pairs)
            throw new NotImplementedException();
        }

        //TODO: Get Characters

        public void AddCookie(Action<bool> asyncReturn)
        {
		    //Empty JSON Request
            AsyncSend(ADD_COOKIE_PATH, new JSONClass(), (j) =>
                {
                    asyncReturn(j["result"].Value == "1");
                });
        }

        public void HasCookie(Action<bool> asyncReturn)
        {
		    //Empty JSON Request
            AsyncSend(HAS_COOKIE_PATH, new JSONClass(), (j) =>
                {
                    asyncReturn(j["result"].Value == "true");
                });
        }
    }

    // From Stack Overflow: http://stackoverflow.com/a/1777246
    class CookieAwareWebClient : WebClient
    {
        public readonly CookieContainer Cookies;

        public CookieAwareWebClient(CookieContainer cookies) {
            this.Cookies = cookies;
        }

        protected override WebRequest GetWebRequest(Uri address)
        {
            WebRequest request = base.GetWebRequest(address);
            HttpWebRequest webRequest = request as HttpWebRequest;
            if (webRequest != null)
            {
                webRequest.CookieContainer = Cookies;
            }
            return request;
        }
    }
}

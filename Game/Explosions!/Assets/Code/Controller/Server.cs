using System;
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
    
	    private const string IS_ALIVE_PATH = "isAlive";
	    private const string LOGIN_REQUEST_PATH = "loginRequest";

	    public const int DEFAULT_TIMEOUT = 10000;

        //TODO: Thread pool?
        //private ThreadPool threads;
        private String url;
        private Dictionary<Character, int> characterIDs;
        private Dictionary<Recipe, int> recipeIDs;


        public Server(String url) 
        {
            this.url = url;
            characterIDs = new Dictionary<Character, int>();
            //characterIDs.Add(Character.SHOP, -1);
            recipeIDs = new Dictionary<Recipe, int>();
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
            var location = this.url + "/" + path;
            var client = new WebClient();
            client.Headers["Content-Type"] = "application/json";
            string response = client.UploadString(location, json.ToString());
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
			    Console.WriteLine(j["result"]);		
			    asyncReturn (j ["result"].Value == "true");
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
            //TODO: Query server for this data
            //TODO: Store these recipes in the recipeIDs dictionary
            throw new NotImplementedException();
        }

        public virtual void GetCraftableItems(Action<IEnumerable<Recipe>> asyncReturn)
        {
            //TODO: Query server for this data
            //TODO: Store these recipes in the recipeIDs dictionary
            throw new NotImplementedException();
        }

	    public virtual void GetMissions(Character character, Action<IEnumerable<Mission>> missions)
	    {
		    //TODO: Query server for this data
		    //This method is actually being called, don't throw an exception
		
		    //AsyncSend ("test", "test", null);
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
    }

}

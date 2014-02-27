using System;
using System.Collections.Generic;
using System.Threading;
using System.Net;
using SimpleJSON;
using Assets.Code.Model;

/// <summary>
/// Only this service should know how to communicate with the server
/// Only this service should know the mapping between Objects and Database IDs
/// </summary>
public class Server 
{
    public const string PRODUCTION_URL = "http://54.200.201.50";
    public const string XEFIER_URL = "http://54.213.248.49/";
    public const string LOCAL_HOST = "localhost";
    private const string IS_ALIVE_PATH = "isAlive";
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
        client.Headers["Content-Type"] = "text/json";
        string response = client.UploadString(location, json);
		return JSON.Parse(response);
    }

    public void IsAlive(Action<bool> asyncReturn)
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
    public void UseRecipe(Recipe recipe, Character inChar, Character outChar, Action<bool> asyncReturn)
    {
        bool success = false;
        //TODO: Retrieve id from recipeIDs (If we don't know about it, then it's not valid)
        //TODO: Call useRecipe on server
        asyncReturn(success);
    }

    public void GetPurchasableItems(Action<IEnumerable<Recipe>> asyncReturn)
    {
        //TODO: Query server for this data
        //TODO: Store these recipes in the recipeIDs dictionary
        throw new NotImplementedException();
    }

    public void GetCraftableItems(Action<IEnumerable<Recipe>> asyncReturn)
    {
        //TODO: Query server for this data
        //TODO: Store these recipes in the recipeIDs dictionary
        throw new NotImplementedException();
    }

	public void GetMissions(Character character, Action<IEnumerable<Mission>> missions)
	{
		//TODO: Query server for this data
		throw new NotImplementedException();
		
		//AsyncSend ("test", "test", null);
	}

    //TODO: Get Characters
}

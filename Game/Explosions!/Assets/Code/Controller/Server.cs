using System;
using System.Collections.Generic;
//using System.Threading.Tasks;

using SimpleJSON;
using System.Collections;
using UnityEngine;
using Assets.Code.Components;
using Assets.Code.Model;
using System.Threading;

/// <summary>
/// Only this service should know how to communicate with the server
/// Only this service should know the mapping between Objects and Database IDs
/// </summary>
public class Server 
{
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
    protected virtual void AsyncSend(string path, string json, Action<JSONNode> asyncReturn)
    {
        //TODO: Use a threadpool instead of creating a new thread every time
        new Thread(() =>
        {
            asyncReturn(Send(path, json));
        });
    }

    /// <summary>
    /// Synchronous Send (Will block caller)
    /// </summary>
    /// <param name="path"></param>
    /// <param name="json"></param>
    /// <returns></returns>
    protected virtual JSONNode Send(String path, String json) 
    {
        var utf8 = new System.Text.UTF8Encoding();
        var header = new Hashtable();
           
        header.Add("Content-Type", "text/json");
        header.Add("Content-Length", json.Length);
         
        var location = this.url + "/" + path;
        var www = new WWW(location, utf8.GetBytes(json), header);
         
		return JSON.Parse(www.text);
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

    //TODO: Get Characters
    //TODO: Get Missions
}

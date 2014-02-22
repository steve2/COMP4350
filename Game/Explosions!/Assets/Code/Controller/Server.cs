using System;
using System.Collections.Generic;
//using System.Threading.Tasks;

using SimpleJSON;
using System.Collections;
using UnityEngine;

public class Server 
{
    private static Server instance;
    private const string PRODUCTION_URL = "http://54.200.201.50";
    private String url;

    /// <summary>
    /// This is a service that provides communication with a "server" (Whether it's a real server or not)
    /// It can be changed during run-time
    /// Options: ProductionServer, TestServer, DemoServer, ServerStub
    /// </summary>
    //public static Server Instance { get; set; }
    //TODO: Maybe instead of instantiating a default server here, we could handle that in a setup elsewhere and use the cleaner property above
    //TODO: This instance can be stored inside Server class, or inside a different global class
    public static Server Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new Server(PRODUCTION_URL);
            }
            return instance;
        }
        set
        {
            instance = value;
        }
    }

    public Server(String url) 
    {
        this.url = url;
    }

    protected virtual JSONNode Send(String path, String json) 
    {
        var utf8 = new System.Text.UTF8Encoding();
        var header = new Hashtable();
           
        header.Add("Content-Type", "text/json");
        header.Add("Content-Length", json.Length);
         
        var location = this.url + "/" + path;
        var www = new WWW(location, utf8.GetBytes(json), header);
         
		return JSON.Parse(www.text);
           
        //return www.text);
    }
}

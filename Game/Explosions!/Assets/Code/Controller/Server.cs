using System;
using System.Collections.Generic;
//using System.Threading.Tasks;

using SimpleJSON;
using System.Collections;
using UnityEngine;

class Server {
    private String url;

    public Server(String url) {
        this.url = url;
    }

    private JSONNode Send(String path, String json) {
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

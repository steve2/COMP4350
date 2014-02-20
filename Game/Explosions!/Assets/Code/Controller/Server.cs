using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using SimpleJSON;

class Server {
    private String url;

    public Server(String url) {
        this.url = url;
        this.serializer = new JavaScriptSerializer();
    }

    private async Task<JSON> Send(String path, String json) {
        var utf8 = new System.Text.UTF8Encoding();
        var header = new Hashtable();
           
        header.Add("Content-Type", "text/json");
        header.Add("Content-Length", json.Length);
         
        var location = this.url + "/" + path;
        var www = WWW(location, utf8.GetBytes(json), header);
         
        yield return www;
           
        return JSON.Parse(www.text);
    }
}

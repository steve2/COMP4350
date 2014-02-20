using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Script.Serialization.JavaScriptSerializer;
using System.Threading.Tasks;

using SimpleJSON;

class Server {
    private String url;
    private JavaScriptSerializer serializer;

    public Server(String url) {
        this.url = url;
        this.serializer = new JavaScriptSerializer();
    }

    public async Task<JSON> Send(String json) {
        var utf8 = new System.Text.UTF8Encoding();
        var header = new Hashtable();
           
        header.Add("Content-Type", "text/json");
        header.Add("Content-Length", json.Length);
         
        var www = WWW(this.url, utf8.GetBytes(json), header);
         
        yield return www;
           
        return JSON.Parse(www.text);
    }
}

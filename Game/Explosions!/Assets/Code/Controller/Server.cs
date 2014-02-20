using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Script.Serialization.JavaScriptSerializer;
using System.Threading.Tasks;

class Server {
    private String url;
    private JavaScriptSerializer serializer;

    public Server(String url) {
        this.url = url;
        this.serializer = new JavaScriptSerializer();
    }

    public async Task<Dictionary<String, Object>> Send(Object table) {
        var json = serializer.serialize(table);
         
        var utf8 = new System.Text.UTF8Encoding();
        var postHeader = new Hashtable();
           
        postHeader.Add("Content-Type", "text/json");
        postHeader.Add("Content-Length", json.Length);
         
        var www = WWW(this.url, utf8.GetBytes(json), postHeader);
         
        yield return www;
           
        return serializer.Deserialize(www.text);
    }
}

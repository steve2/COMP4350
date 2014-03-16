using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Code.Components;
using Assets.Code.Controller;

public class MainMenu : GameComponent {

    //Called before Start
    void Awake()
    {
        Game.Init();
    }

	// Use this for initialization
	public override void Start () 
    {
        base.Start();
        Game.Instance.IsServerOnline((x) =>
        {
            Debug.Log("Server is " + (x ? "Online" : "Offline"));
        });
		Game.Instance.TestGetInventory ((inventory) => 
		{		                          
			foreach (KeyValuePair<string, int> entry in inventory)
			{
				Debug.Log ("Obtained Item " + entry.Key + " with quantity " + entry.Value);
			}
		});
	} 

	// Called when the GUI is rendered
	void OnGUI() {
		GUILayout.BeginArea (new Rect ((Screen.width * 0.5f) - 100, (Screen.height * 0.5f) - 100, 200, 300));

		// Load the Mission Menu
		if (GUILayout.Button ("Single Player", GUILayout.Height(40))) {
			Application.LoadLevel("PlayerLogin"); // Load the Mission List
		}

		if (GUILayout.Button ("Play Demo", GUILayout.Height(40))) {
			Application.LoadLevel("Demo"); // Play the Demo 
		}

		// Load the Website
		if (GUILayout.Button ("View Website", GUILayout.Height(40))) 
		{
			//Application.OpenURL("http://54.200.201.50/");
			Application.OpenURL(Server.PRODUCTION_URL + "/");
		}

		// Load the Settings 
		if (GUILayout.Button ("Settings", GUILayout.Height(40))) 
		{
			Application.LoadLevel("Settings"); 
		}
		GUILayout.EndArea();
	}
}

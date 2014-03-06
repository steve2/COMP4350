﻿using UnityEngine;
using System.Collections;
using Assets.Code.Components;
using Assets.Code.Model;
using Assets.Code.Controller;

public class MissionMenu : GameComponent {

	// Use this for initialization
	public override void Start () {
        base.Start();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// Called when the GUI is rendered
	void OnGUI() {
		GUILayout.BeginArea (new Rect ((Screen.width * 0.5f) - 100, (Screen.height * 0.5f) - 100, 200, 300));

		// Load Mission list for the character

		foreach (Mission mission in Game.Missions) 
		{
			if (GUILayout.Button ("Mission" + mission.ID)) 
			{
					Application.LoadLevel ("Demo"); 
			}
		}

		// TODO: Populate Mission Description	


		// Temporary (Testing)
		// Just load the Demo for each Mission
		if (GUILayout.Button ("Mission 1", GUILayout.Height(40))) {
			Application.LoadLevel("Demo"); 
		}
		if (GUILayout.Button ("Mission 2", GUILayout.Height(40))) {
			Application.LoadLevel("Demo");  
		}
		if (GUILayout.Button ("Mission 3", GUILayout.Height(40))) {
			Application.LoadLevel("Demo");  
		}
		if (GUILayout.Button ("Mission 4", GUILayout.Height(40))) {
			Application.LoadLevel("Demo"); 
		}

		GUILayout.EndArea();
	}
}

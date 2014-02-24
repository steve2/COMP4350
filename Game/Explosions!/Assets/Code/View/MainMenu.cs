using UnityEngine;
using System.Collections;
using Assets.Code.Components;

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
	}

	// Called when the GUI is rendered
	void OnGUI() {
		GUILayout.BeginArea (new Rect ((Screen.width * 0.5f) - 100, (Screen.height * 0.5f) - 100, 200, 300));
		if (GUILayout.Button ("Single Player")) {
			Application.LoadLevel("MissionMenu"); // Load the Mission List
		}
		GUILayout.Button ("Multiplayer");
		if (GUILayout.Button ("Play Demo")) {
			Application.LoadLevel("Demo"); // Play the Demo 
		}
		GUILayout.Button ("View website");
		GUILayout.Button ("Settings");
		GUILayout.EndArea();
	}
}

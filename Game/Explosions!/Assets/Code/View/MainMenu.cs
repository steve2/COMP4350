using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
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

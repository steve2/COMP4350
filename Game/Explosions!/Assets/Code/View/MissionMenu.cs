using UnityEngine;
using System.Collections;
using Assets.Code.Components;

public class MissionMenu : GameComponent {

	private Character character;

	// Use this for initialization
	public override void Start () {
        base.Start();
		character = GetComponent<Character> (); 
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// Called when the GUI is rendered
	void OnGUI() {
		GUILayout.BeginArea (new Rect ((Screen.width * 0.5f) - 100, (Screen.height * 0.5f) - 100, 200, 300));

		// TODO: Load Mission list for the character
		// TODO: Populate Mission Description

		// Temporary (Testing)
		// Just load the Demo for each Mission
		if (GUILayout.Button ("Mission 1")) {
			Application.LoadLevel("Demo"); 
		}
		if (GUILayout.Button ("Mission 2")) {
			Application.LoadLevel("Demo");  
		}
		if (GUILayout.Button ("Mission 3")) {
			Application.LoadLevel("Demo");  
		}
		if (GUILayout.Button ("Mission 4")) {
			Application.LoadLevel("Demo"); 
		}

		GUILayout.EndArea();
	}
}

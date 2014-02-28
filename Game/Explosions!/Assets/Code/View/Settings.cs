using UnityEngine;
using System.Collections;

public class Settings : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// Called when the GUI is rendered
	void OnGUI() {
		GUILayout.BeginArea (new Rect ((Screen.width * 0.5f) - 100, (Screen.height * 0.5f) - 100, 200, 300));

		if (GUILayout.Button ("Back", GUILayout.Height(40))) {
			Application.LoadLevel("MainMenu"); 
		}
		
		GUILayout.EndArea();
	}
}

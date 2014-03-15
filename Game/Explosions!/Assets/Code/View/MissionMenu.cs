using UnityEngine;
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
		GUILayout.Label ("---- Select a mission ----");

		// TODO: This currently does not work
		foreach (Mission mission in Game.Missions) 
		{
			if (GUILayout.Button ("Mission" + mission.ID)) 
			{
				Application.LoadLevel ("Demo"); 
			}
		}	

		if (GUILayout.Button ("Shop")) 
		{
			Application.LoadLevel ("ShopMenu"); 
		}
		// Hardcoding a mission for now
		if (GUILayout.Button ("Mission 1")) 
		{
			Application.LoadLevel ("Demo"); 
		}
		GUILayout.EndArea();
	}
}

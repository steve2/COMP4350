using UnityEngine;
using System;
using System.Collections;
using Assets.Code.Components;
using Assets.Code.Model;
using System.Collections.Generic;

public class CharacterSelection : GameComponent {
	
	// Use this for initialization
	public override void Start () {
		base.Start();
	}

	//Called when GUI is rendered
	void OnGUI() {
		GUILayout.BeginArea (new Rect ((Screen.width * 0.5f) - 100, (Screen.height * 0.5f) - 100, 200, 300));
		GUILayout.Label ("---- Select a character ----");

		// Load character list for selection
		foreach (Character character in Game.Characters) 
		{
			if (GUILayout.Button (character.Name)) 
			{
				base.Game.character = character;
				InvokeOnMainThread(() => Application.LoadLevel("MissionMenu"));
			}
		}
		if (GUILayout.Button ("Back")) {
			InvokeOnMainThread(() => Application.LoadLevel("PlayerLogin"));
		}

		GUILayout.EndArea();
	}
}

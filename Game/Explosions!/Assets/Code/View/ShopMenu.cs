using UnityEngine;
using System;
using System.Collections;
using Assets.Code.Model;
using Assets.Code.Components;
using Assets.Code.Controller;

public class ShopMenu : GameComponent{

	// Use this for initialization
	public override void Start () {
			base.Start();

	}
	
	// Update is called once per frame
	void OnGUI() {
		GUILayout.BeginArea (new Rect ((Screen.width * 0.5f) - 100, (Screen.height * 0.5f) - 200, 200, 300));
		GUILayout.Label ("---- Welcome to the Shop ----");

		foreach (Recipe recipe in Game.PurchasableItems) 
		{
			if (GUILayout.Button (recipe.Name)) 
			{

			}
		}

		// Load character list for selection
		if (GUILayout.Button ("Back")) {
				InvokeOnMainThread (() => Application.LoadLevel ("MissionMenu"));
		}

		GUILayout.EndArea ();
	}
}


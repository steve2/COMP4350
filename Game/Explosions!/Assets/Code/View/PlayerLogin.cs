﻿using UnityEngine;
using System;
using System.Collections;
using Assets.Code.Components;
using Assets.Code.Model;

public class PlayerLogin : GameComponent {
	private string username;
	private string password;
	private string output;
	
	// Use this for initialization
	public override void Start () {
		base.Start();
		
		username = "";
		password = "";
		output = "Please enter username and password.";
	}
	
	//Called when GUI is rendered
	void OnGUI() {
		GUILayout.BeginArea (new Rect ((Screen.width * 0.5f) - 100, (Screen.height * 0.5f) - 100, 200, 300));
		
		username = GUILayout.TextField (username, 50);
		password = GUILayout.TextField (password, 50);
		if (GUILayout.Button ("Login")) {
			this.attemptLogin();	
		}
		if (GUILayout.Button ("Back")) {
			GameInst.LoadLevel("MainMenu");
		}
		
		GUILayout.Label (output);
		GUILayout.EndArea();
	}
	
	private void attemptLogin(){
		base.GameInst.Authenticate(
			this.username, 
			this.password, 
			((x) =>
		 {
			if (x){
				output = "Login successful";

				GameInst.username = username;
				GameInst.LoadLevel("CharacterSelection"); //CharacterSelection loads "Demo"
			} else {
				output = "Login failed";				
			}
		}));
	}
	
}
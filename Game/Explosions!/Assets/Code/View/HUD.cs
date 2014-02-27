﻿using UnityEngine;
using System.Collections;
using Assets.Code.Components;

// The HUD(Heads-Up Display)

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Experience))]
public class HUD : MonoBehaviour {

	private Rect healthBarRect;	// Health Bar
	private Rect expBarRect;	// EXP Bar

	private Texture2D blank;
	private float guiOpacity;

	private Health health;
	private Experience exp;

	// Use this for initialization
	void Start () 
	{
		healthBarRect = new Rect (0, 0, 200, 15);
		expBarRect = new Rect (0, 15, 200, 15);
		
		health = GetComponent<Health> ();
		exp = GetComponent<Experience> ();

		blank = new Texture2D (1, 1);
		guiOpacity = 1;
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	void OnGUI() 
	{
		LoadHealthBar (healthBarRect);
		LoadExpBar (expBarRect);

		//LoadBar (healthBarRect, health.PercentHealth);
		//LoadBar (expBarRect, exp.PercentEXP);
	}

	private void LoadHealthBar(Rect rect)
	{
		float percent = health.PercentHealth; 
		float x = rect.x;
		float y = rect.y;
		float width = rect.width;
		float height = rect.height;
		float newWidth = width * percent;

		SetColor (Color.Lerp (Color.red, Color.green, percent));
		GUI.DrawTexture (new Rect (x, y, newWidth, height), blank);

		if (percent < 1) 
		{
			var clr = Color.grey;
			clr.a = 0.2f;
			SetColor(clr);
			GUI.DrawTexture(new Rect(x + newWidth, rect.y, width * (1.0f - percent), height), blank);
		}

		ResetColor ();
	}

	// TODO
	private void LoadExpBar(Rect rect)
	{
		GUI.DrawTexture (new Rect (rect.x, rect.y, rect.width, rect.height), blank);
	}

	// TODO
	private void LoadBar(Rect rect, float? barPercent)
	{
		float percent;

		if (barPercent.HasValue) 
		{
			percent = barPercent.Value;
		} else {
			percent = 1;
		}

		float x = rect.x;
		float y = rect.y;
		float width = rect.width;
		float height = rect.height;
		float newWidth = width * percent;
		
		SetColor (Color.Lerp (Color.red, Color.green, percent));
		GUI.DrawTexture (new Rect (x, y, newWidth, height), blank);
		
		if (percent < 1) 
		{
			var clr = Color.grey;
			clr.a = 0.2f;
			SetColor(clr);
			GUI.DrawTexture(new Rect(x + newWidth, rect.y, width * (1.0f - percent), height), blank);
		}
		
		ResetColor ();
	}

	private void SetColor(Color color)
	{
		color.a = Mathf.Min (color.a, guiOpacity);
		GUI.color = color;
	}

	private void ResetColor()
	{
		SetColor (Color.white);
	}
}

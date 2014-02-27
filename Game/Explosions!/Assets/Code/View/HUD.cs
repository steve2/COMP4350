using UnityEngine;
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
		LoadHealthBar ();
		LoadExpBar ();
	}

	private void LoadHealthBar()
	{
		float percent = health.PercentHealth; 
		float x = healthBarRect.x;
		float y = healthBarRect.y;
		float width = healthBarRect.width;
		float height = healthBarRect.height;
		float newWidth = width * percent;

		SetColor (Color.Lerp (Color.red, Color.green, percent));
		GUI.DrawTexture (new Rect (x, y, newWidth, height), blank);

		if (percent < 1) 
		{
			var clr = Color.grey;
			clr.a = 0.2f;
			SetColor(clr);
			GUI.DrawTexture(new Rect(x + newWidth, healthBarRect.y, width * (1.0f - percent), height), blank);
		}

		ResetColor ();
	}

	// TODO: Use percentEXP to draw the experience bar
	private void LoadExpBar()
	{
		float percentEXP = exp.PercentEXP; // Not used yet...
		GUI.DrawTexture (new Rect (expBarRect.x, expBarRect.y, expBarRect.width, expBarRect.height), blank);
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

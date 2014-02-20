using UnityEngine;
using System.Collections;

public class MovementButtons : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}

	// Called when the GUI is rendered
	void OnGUI() {
		GUILayout.BeginArea (new Rect (0, (Screen.height) - 100, 100, 100));
		if (GUILayout.RepeatButton ("W")) 
		{
			InputAdapter.SetAxisRaw ("Vertical", 1);
			GUILayout.RepeatButton ("S");
		} 
		else if (GUILayout.RepeatButton ("S")) 
		{
			InputAdapter.SetAxisRaw ("Vertical", -1);
		} 
		else
		{
			InputAdapter.SetAxisRaw ("Vertical", 0);
		}
		
		if (GUILayout.RepeatButton ("A"))
		{
			InputAdapter.SetAxisRaw ("Horizontal", -1);
			GUILayout.RepeatButton("D");
		}
		else if ( GUILayout.RepeatButton("D"))
		{
			InputAdapter.SetAxisRaw("Horizontal", 1);
		}
		else
		{
			InputAdapter.SetAxisRaw("Horizontal", 0);
		}
        GUILayout.EndArea();
    }
}
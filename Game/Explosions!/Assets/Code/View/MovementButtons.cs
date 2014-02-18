using UnityEngine;
using System.Collections;

public class MovementButtons : MonoBehaviour {
//	private static float currVerticalInputGUI = 0.0f;
//
//
//	private static float verticalInputGUI = 0.0f;
//	private static float verticalInputKey = 0.0f;
//	private static float verticalInputFinal = 0.0f;
//	private static float verticalInput = 0.0f;
//
//	private static float horizontalInputGUI = 0.0f;
//	private static float horizontalInput = 0.0f;
//	private static float horizontalFinal = 0.0f;
//	private static float horizontalInputKey = 0.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
//		verticalInputKey = Input.GetAxis( "Vertical" );
//		verticalInputFinal = Mathf.Clamp (verticalInputKey + verticalInputGUI, -1.0f, 1.0f);
//		horizontalInputKey = Input.GetAxis ("Horizontal");
//		vertInputKey = Input.GetAxis( "Vertical" );		
//		vertInputAll = Mathf.Clamp( vertInputKey + vertInputGUI, -1.0, 1.0 );  
//		transform.position += transform.forward * vertInputAll * speed * Time.deltaTime;
		
	}

	// Called when the GUI is rendered
	void OnGUI() {
//		var controller : CharacterController  = GetComponent (CharacterController );
//		GUILayout.BeginArea (new Rect ((Screen.width * 0.5f) - 100, (Screen.height * 0.5f) - 100, 200, 300));
//		GUI.Box (new Rect (Screen.width - 100,Screen.height - 50,100,50));

        GUILayout.BeginArea(new Rect(0, (Screen.height) - 100, 100, 100));

        if (GUILayout.RepeatButton("W"))
        {
            InputAdapter.SetAxisRaw("Vertical", 1);
        }
        else
        {
            InputAdapter.SetAxisRaw("Vertical", 0);
        }

        GUILayout.EndArea();
    }
}


		//		if ( GUI.RepeatButton( new Rect( 10, 10, 100, 35 ), "W" ) )
		//		{
		//			Event.KeyboardEvent("" + KeyCode.W);
		//		}
		//		else if ( GUI.RepeatButton(new Rect( 10, 55, 100, 35 ), "S" ) )
		//		{
		//			verticalInputGUI = -1.0f;
		//		}
		//		else
		//		{
		//			verticalInputGUI = 0.0f;
		//		}

		//		if (GUILayout.Button ("A")) {
		//			Event.KeyboardEvent("" + KeyCode.A);
		//		}
		
		//		if (GUILayout.Button ("S")) {
		//			Event.KeyboardEvent(KeyCode.S);
		//		}
		//		if (GUILayout.Button ("A")) {
		//			Event.KeyboardEvent(KeyCode.A);
		//		}
		//		if (GUILayout.Button ("D")) {
		//			Event.KeyboardEvent(KeyCode.D);
		//		}
//	}
//}
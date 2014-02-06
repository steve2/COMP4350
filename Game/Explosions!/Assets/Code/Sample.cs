using UnityEngine;
using System.Collections;

public class Sample : MonoBehaviour {

    //This field is not visible in the inspector
    private int privateField;

    //This field is visible in the inspector
    [SerializeField]
    private int inspectorField;

	// Use this for initialization (Only called once)
	void Start ()
    {
	    
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    // Fixed(Physics) Update is called at a fixed interval
    void FixedUpdate()
    {

    }
}

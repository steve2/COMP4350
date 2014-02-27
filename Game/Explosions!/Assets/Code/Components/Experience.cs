using UnityEngine;
using System.Collections;

// For use with the EXP bar in the HUD
public class Experience : MonoBehaviour {

	private float percentEXP;

	// Use this for initialization
	void Start () 
	{
		percentEXP = 100;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public float PercentEXP
	{
	    get
	    {
	        return percentEXP;
	    }
	}

	public void increaseEXP(float increase)
	{
		percentEXP += increase;
	}
}

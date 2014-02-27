﻿using UnityEngine;
using System.Collections;

// For use with the Health bar in the HUD
public class Health : MonoBehaviour {

	private float percentHealth;

	// Use this for initialization
	void Start () {
		percentHealth = 100;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public float PercentHealth
	{
	    get
	    {
	        return percentHealth;
	    }
	}

	// Take some damage
	public void reduceHealth(float reduction) 
	{
		percentHealth -= reduction;
		if (percentHealth < 0) 
		{
			percentHealth = 0;
		}
	}
	
	public bool increaseHealth (float increase)
	{
		if (percentHealth != 0) 
		{
			percentHealth += increase;
			if (percentHealth > 100)
			{
				percentHealth = 100;
			}
			return true;
		}

		// Return false if the health is empty
		// We will not allow any more health to be added 
		return false;
	}

	// A bool signifying empty health (i.e., Dead)
	public bool isEmpty() {
		return (percentHealth == 0);
	}
}
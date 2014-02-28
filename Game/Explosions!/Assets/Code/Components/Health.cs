using UnityEngine;
using System.Collections;

// For use with the Health bar in the HUD
public class Health : MonoBehaviour {

	private float percentHealth;

	public Health()
	{
		percentHealth = 1;
	}

	// Use this for initialization
	void Start () {
		percentHealth = 1; // Start with full health
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
	public void decreaseHealth(float decrease) 
	{
		percentHealth -= decrease;
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
			if (percentHealth > 1)
			{
				percentHealth = 1f;
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

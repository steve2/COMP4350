using UnityEngine;
using System.Collections;
using Assets.Code.Components;
using Assets.Code.Model;

// For use with the Health bar in the HUD
[RequireComponent(typeof(AttributeManager))]
public class Health : MonoBehaviour {

    [SerializeField]
	private int health;
    [SerializeField] 
	private int maxHealth;

	private AttributeManager attributeMngr;

	// This constructor will be used for testing
	public Health()
	{
		health = 100;
		maxHealth = 100;
	}

	// Use this for initialization
	void Start () {
		attributeMngr = GetComponent<AttributeManager>();

		// Get max health from the attribute manager
		maxHealth = attributeMngr.GetAttributeValue (AttributeType.Health);
        health = maxHealth;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public float PercentHealth
	{
	    get
	    {
	        return (float)health / (float)maxHealth;
	    }
	}

	// Take some damage
	public void decreaseHealth(int decrease) 
	{
		health -= decrease;
		if (health < 0) 
		{
			health = 0;
		}
	}
	
	public bool increaseHealth (int increase)
	{
		if (health > 0) 
		{
			health += increase;
			if (health > maxHealth)
			{
				health = maxHealth;
			}
			return true;
		}

		// Return false if the health is empty
		// We will not allow any more health to be added 
		return false;
	}

	// A bool signifying empty health (i.e., Dead)
	public bool isEmpty() {
		return (health == 0);
	}
}

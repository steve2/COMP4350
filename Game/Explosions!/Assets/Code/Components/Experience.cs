using UnityEngine;
using System.Collections;

// For use with the EXP bar in the HUD
public class Experience : MonoBehaviour {

	private int currentEXP;
	private int maxEXP;

	public Experience()
	{
		currentEXP = 0;
		maxEXP = 100;
	}

	// Use this for initialization
	void Start () 
	{
		currentEXP = 0; 
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// Start at level 1
	// Increase level for every 100 experienct points gained
	// TODO: Implement a way to determine how much exp is required for next level
	public int Level
	{
		get
		{
			return (currentEXP / maxEXP) + 1;
		}
	}

	// TODO: Change this once we change how much experience is required for each level
	public float PercentEXP
	{
		get 
		{
			return (float)(currentEXP % 100) / (float)maxEXP;
		}
	}
	
	public int CurrentEXP
	{
	    get
	    {
	        return currentEXP;
	    }
	}

	public void IncreaseEXP(int increase)
	{
		currentEXP += increase;
	}

	public void DecreaseEXP(int decrease)
	{
		currentEXP -= decrease;
		if (currentEXP < 0) 
		{
			currentEXP = 0;
		}
	}
}

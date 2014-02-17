using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour 
{
    #region Fields
    private int health;
    private int maxHealth;
    [SerializeField]
    private int exp;
    [SerializeField]
    private int level; 
    #endregion

    #region Properties
    #region PercentHealth
    public int PercentHealth
    {
        get
        {
            return (int)((float)health / (float)maxHealth) * 100;
        }
    } 
    #endregion

    public int Exp { get { return exp; } }

    public int Level { get { return level; } } 
    #endregion

	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

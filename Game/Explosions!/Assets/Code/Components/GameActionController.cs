using UnityEngine;
using System.Collections;
using Assets.Code.Components.Actions;
using System.Collections.Generic;

/// <summary>
/// Maps controls to actions
/// Actions can also be performed directly by name
/// </summary>
[RequireComponent(typeof(GameActionManager))]
public class GameActionController : MonoBehaviour, IEnumerable<string>
{

    //TODO: Allow assigning hotkeys to actions (PC)
    private GameActionManager mgr;

	// Use this for initialization
	void Start () 
    {
        mgr = GetComponent<GameActionManager>();
	}

    public void Perform(string name)
    {
        mgr.Perform(name);
    }

    public IEnumerator<string> GetEnumerator()
    {
        return mgr.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

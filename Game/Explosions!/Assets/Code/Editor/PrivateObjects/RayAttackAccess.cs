using UnityEngine;
using System.Collections;
using Assets.Code.Components.Actions;

/// <summary>
/// Similar to a PrivateObject, except it only works on protected member
/// </summary>
public class RayAttackAccess : RayAttack 
{
    public int _ApplyDamage(GameObject go, int damage)
    {
        return ApplyDamage(go, damage);
    }
}

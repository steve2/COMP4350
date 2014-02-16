#if UNITY_EDITOR
using UnityEngine;
using System.Collections;
using NUnit.Framework;
using Assets.Code.Components;
using Assets.Code.Model;
using System.Collections.Generic;

[ExecuteInEditMode]
public class TestWeapon {

    //Make sure the Weapon is being initialized properly (default)
    [Test]
    public void TestDefaultStart()
    {
        Weapon w = new Weapon();
        w.extraAttributes = new List<ItemAttribute>(); //Normally this will be initialized automatically for an in-game instance
        w.Start();
        Item i = w.Item;
        Assert.That(i.GetAttribute(AttributeType.ItemType).Value == (int)ItemType.Weapon);
        Assert.That(i.GetAttribute(AttributeType.Damage).Value == 0);
    }
}
#endif
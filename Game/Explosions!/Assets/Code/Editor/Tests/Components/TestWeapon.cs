#if UNITY_EDITOR
using UnityEngine;
using System.Collections;
using NUnit.Framework;
using Assets.Code.Components;
using Assets.Code.Model;
using System.Collections.Generic;
using System.Linq;

[ExecuteInEditMode]
public class TestWeapon {
    //TODO: Should consider making Weapon.Attributes and Weapon constructor internal and add InternalsVisibleTo

    //Make sure the Weapon is being initialized properly (default)
    [Test]
    public void TestDefaultStart()
    {
        //This is normally done automatically, but where peforming a Unit Test
        Weapon.Attributes a = new Weapon.Attributes();
        a.damage = 0;
        a.extraAttributes = new List<ItemAttribute>();

        Weapon w = new Weapon(a); //IGNORE WARNING

        w.Start();
        //Iterating through the list to find an item is inefficient,
        //but this is a Unit Test and we don't want to give public read/write access to the attributes
        IEnumerable<ItemAttribute> i = w.ItemAttributes; 
        Assert.That(i.Single((x) => x.Type == AttributeType.ItemType).Value == (int)ItemType.Weapon);
        Assert.That(i.Single((x) => x.Type == AttributeType.Damage).Value == 0);
    }
}
#endif
#if UNITY_EDITOR
using UnityEngine;
using System.Collections;
using NUnit.Framework;
using Assets.Code.Components;
using Assets.Code.Model;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Code.Editor.Tests.Components
{
    [ExecuteInEditMode]
    class TestWeapon
    {
        //TODO: Should consider making Weapon.Attributes and Weapon constructor internal and add InternalsVisibleTo

        //Make sure the Weapon is being initialized properly (default)
        [Test]
        public void TestDefaultStart()
        {
            //This is normally done automatically, but where peforming a Unit Test
            Weapon.EditorAttributes eAttributes = new Weapon.EditorAttributes();
            eAttributes.damage = 0;
            eAttributes.extraAttributes = new List<GameAttribute>();

            Item item = new Weapon(eAttributes); //IGNORE WARNING

            item.Start();
            //Iterating through the list to find an item is inefficient,
            //but this is a Unit Test and we don't want to give public read/write access to the attributes
            IEnumerable<GameAttribute> attributes = item.ItemAttributes;
            //Assert.AreEqual((int)ItemType.Weapon, attributes.Single((x) => x.Type == AttributeType.ItemType).Value);
            Assert.AreEqual(0, attributes.Single((x) => x.Type == AttributeType.Damage).Value);
        }
    }
}
#endif
#if UNITY_EDITOR
using UnityEngine;
using System.Collections;
using NUnit.Framework;
using Assets.Code.Components;
using System.Collections.Generic;
using Assets.Code.Model;

namespace Assets.Code.Editor.Tests.Components
{
    [ExecuteInEditMode]
    class TestInventory
    {
        private GameObject CreateOwner()
        {
            GameObject go = new GameObject();
            go.AddComponent<Inventory>();
            go.AddComponent<AttributeManager>(); //Required Component
            //TODO: Find a way to get Unity to fully initialize these components
            go.GetComponent<AttributeManager>().Start();
            go.GetComponent<Inventory>().Start();
            return go;
        }

        private Item CreateWeapon(int damage, int speed, int range)
        {
            Weapon.EditorAttributes eAttributes = new Weapon.EditorAttributes();
            eAttributes.damage = damage;
            eAttributes.speed = speed;
            eAttributes.range = range;
            eAttributes.extraAttributes = new List<ItemAttribute>();

            Weapon ret = new Weapon(eAttributes);
            ret.Start();
            return ret;
        }

        [Test]
        //TODO: Maybe this should be setup as an Integration Test instead of a Unit Test
        public void TestEquipSingleAttributes()
        {
            int expectedDamage = 30;
            int expectedSpeed = 20;
            int expectedRange = 10;

            GameObject owner = CreateOwner();
            Inventory inv = owner.GetComponent<Inventory>();
            AttributeManager mgr = owner.GetComponent<AttributeManager>();
            Item item = CreateWeapon(expectedDamage, expectedSpeed, expectedRange);
            inv.Equip(item);

            Assert.AreEqual(expectedDamage, mgr.GetAttributeValue(AttributeType.Damage));
            Assert.AreEqual(expectedSpeed, mgr.GetAttributeValue(AttributeType.Speed));
            Assert.AreEqual(expectedRange, mgr.GetAttributeValue(AttributeType.Range));
        }

        //TODO: Test multiple

        [Test]
        public void TestDequipSingle()
        {
            GameObject owner = CreateOwner();
            Inventory inv = owner.GetComponent<Inventory>();
            AttributeManager mgr = owner.GetComponent<AttributeManager>();
            Item item = CreateWeapon(1, 2, 3);
            inv.Equip(item);
            inv.Dequip(item);
            Assert.AreEqual(0, mgr.GetAttributeValue(AttributeType.Damage));
            Assert.AreEqual(0, mgr.GetAttributeValue(AttributeType.Speed));
            Assert.AreEqual(0, mgr.GetAttributeValue(AttributeType.Range));
        }

        //TODO: Test multiple
    }
}
#endif
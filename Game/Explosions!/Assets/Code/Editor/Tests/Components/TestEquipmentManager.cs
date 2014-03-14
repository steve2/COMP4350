#if UNITY_EDITOR
using UnityEngine;
using System.Collections;
using NUnit.Framework;
using Assets.Code.Components;
using System.Collections.Generic;
using Assets.Code.Model;
using Assets.Code.Components.Actions;
using Assets.Code.Editor.Stubs;

namespace Assets.Code.Editor.Tests.Components
{
    [ExecuteInEditMode]
    class TestEquipmentManager
    {
		private GameObject testGameObject;
		private Item[] testItems;
		private GameAttribute[] testAttributes;

        //TODO: Create IntegrationTests that actually Create/Destroy prefabs 

		/** Setup **/
		private void Setup()
		{
			//Setup Game Object and necessary Components for EquipmentManagerStub.
			testGameObject = new GameObject();
			testItems = new Item[4];
			testAttributes = new GameAttribute[4];

            GameActionManager actionMgr = testGameObject.AddComponent<GameActionManager>();
            actionMgr.Awake();
            actionMgr.Start();
			testGameObject.AddComponent<AttributeManager>().Awake ();
			testGameObject.AddComponent<Inventory>().Start ();
            testGameObject.AddComponent<EquipmentManagerStub>().Start();

			//Setup Items that can be used to Equip/Unequip.
			testItems[0] = testGameObject.AddComponent<Item>();
			testItems[0].Awake ();
			testItems[0].name = "Item 01";
			testItems[0].Type = ItemType.Weapon;

			testItems[1] = testGameObject.AddComponent<Item>();
			testItems[1].Awake ();
			testItems[1].name = "Item 02";
			testItems[1].Type = ItemType.Chest;

			testItems[2] = testGameObject.AddComponent<Item>();
			testItems[2].Awake ();
			testItems[2].name = "Item 03";
			testItems[2].Type = ItemType.Legs;

			testItems[3] = testGameObject.AddComponent<Item>();
			testItems[3].Awake ();
			testItems[3].name = "Zapp Cannon";
			testItems[3].Type = ItemType.Weapon;

			//Setup Item Attributes for more thorough testing.
			testAttributes[0] = new GameAttribute(AttributeType.Range, 5);
			testAttributes[1] = new GameAttribute(AttributeType.Capacity, 5);
			testAttributes[2] = new GameAttribute(AttributeType.Health, 5);
			testAttributes[3] = new GameAttribute(AttributeType.Speed, 5);

            //Apply attributes and types to the Items.
			for (int i=0; i < 4; i++)
			{
				testItems[i].AddAttribute(testAttributes[i]);
			}
		}

        private void TestEquippingImpl()
        {
            EquipmentManagerStub equipMgr = testGameObject.GetComponent<EquipmentManagerStub>();
            Inventory inventory = testGameObject.GetComponent<Inventory>();

            inventory.Add(testItems[0]);
            inventory.Add(testItems[0]); //2 of this item in inventory
            inventory.Add(testItems[1]);

            //Cannot equip a NULL item.
            Assert.False(equipMgr.Equip((Item)null, Slot.Head));

            Assert.True(equipMgr.Equip(testItems[0], Slot.RightHand));

            //Try to Equip to wrong Slot:
            Assert.False(equipMgr.Equip(testItems[0], Slot.Chest));
            Assert.False(equipMgr.Equip(testItems[0], Slot.Head));
            Assert.False(equipMgr.Equip(testItems[1], Slot.RightHand));
            Assert.False(equipMgr.Equip(testItems[1], Slot.LeftHand));

            Assert.True(equipMgr.Equip(testItems[0], Slot.LeftHand));

            //No longer has any "item[0]" in Inventory so cannot Equip again.
            Assert.False(inventory.Contains(testItems[0]));
            Assert.False(equipMgr.Equip(testItems[0], Slot.RightHand));

            Assert.True(equipMgr.Equip(testItems[1], Slot.Chest));
            Assert.False(inventory.Contains(testItems[1]));
        }

        private void TestDequippingImpl()
        {
            EquipmentManagerStub equipMgr = testGameObject.GetComponent<EquipmentManagerStub>();
            Inventory inventory = testGameObject.GetComponent<Inventory>();

            inventory.Add(testItems[0]);
            inventory.Add(testItems[0]);
            inventory.Add(testItems[1]);

            Assert.True(equipMgr.Equip(testItems[0], Slot.RightHand));
            Assert.True(equipMgr.Equip(testItems[0], Slot.LeftHand));
            Assert.True(equipMgr.Equip(testItems[1], Slot.Chest));

            Assert.False(inventory.Contains(testItems[0]));
            Assert.False(inventory.Contains(testItems[1]));

            Assert.True(equipMgr.Dequip(Slot.RightHand));
            Assert.True(inventory.Contains(testItems[0]));

            Assert.True(equipMgr.Dequip(Slot.Chest));
            Assert.True(inventory.Contains(testItems[1]));

            Assert.False(equipMgr.Dequip(Slot.RightHand));
            Assert.False(equipMgr.Dequip(Slot.Chest));
            Assert.False(equipMgr.Dequip(Slot.Head));
            Assert.False(equipMgr.Dequip(Slot.Legs));

            Assert.True(equipMgr.Dequip(Slot.LeftHand));
        }

		[Test]
		public void TestEquipping()
		{
            Setup();
            TestEquippingImpl();

		}

		[Test]
		public void TestDequipping()
		{
            Setup();
            TestDequippingImpl();
		}

        //TODO:
        private Item CreateActionItem(string itemName, params string[] actionNames)
        {
            GameObject itemgo = new GameObject();
            Item item = itemgo.AddComponent<Item>();
            item.name = itemName;
            foreach (string name in actionNames)
            {
                GameActionStub action = itemgo.AddComponent<GameActionStub>();
                action.SetName(name);
                action.Start();
            }
            item.Awake();
            return item;
        }

        [Test]
        public void TestEquipping_OneAction()
        {
            Setup();

            EquipmentManagerStub equipMgr = testGameObject.GetComponent<EquipmentManagerStub>();
            Inventory inventory = testGameObject.GetComponent<Inventory>();
            GameActionManager actMgr = testGameObject.GetComponent<GameActionManager>();
            Item item = CreateActionItem("item1", "test");
			item.Type = ItemType.Weapon;
            inventory.Add(item);

            Assert.AreEqual(0, actMgr.Count);
            equipMgr.Equip(item, Slot.RightHand);
            Assert.AreEqual(1, actMgr.Count);
        }

        //TODO:
        //[Test]
        //public void TestInitPrefab()
        //{

        //}

        //[Test]
        //public void TestDestroyPrefab()
        //{

        //}
 
    }
}
#endif
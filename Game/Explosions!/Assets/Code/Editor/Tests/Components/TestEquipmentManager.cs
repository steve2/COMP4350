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
    class TestEquipmentManager
    {
		private GameObject testGameObject;
		private Item[] testItems;
        private ItemType[] testItemTypes;
		private GameAttribute[] testAttributes;

		/** Setup **/
		private void Setup()
		{
			//Setup Game Object and necessary Components for EquipmentManager.
			testGameObject = new GameObject();
			testItems = new Item[4];
            testItemTypes = new ItemType[2];
			testAttributes = new GameAttribute[4];

			testGameObject.AddComponent<AttributeManager>().Start ();
			testGameObject.AddComponent<Inventory>().Start ();
			testGameObject.AddComponent<EquipmentManager>().Start ();

			//Setup Items that can be used to Equip/Unequip.
			testItems[0] = testGameObject.AddComponent<Item>();
			testItems[0].Start ();
			testItems[0].name = "Item 01";
			testItems[1] = testGameObject.AddComponent<Item>();
			testItems[1].Start ();
			testItems[1].name = "Item 02";
			testItems[2] = testGameObject.AddComponent<Item>();
			testItems[2].Start ();
			testItems[2].name = "Item 03";
			testItems[3] = testGameObject.AddComponent<Item>();
			testItems[3].Start ();
			testItems[3].name = "Item 04";

            //Setup Item Types for more thorough testing.
            testItemTypes[0] = testGameObject.AddComponent<ItemType>();
            testItemTypes[0].Start();
            testItemTypes[0].SetTypeInfo(1, "Weapon");
            testItemTypes[0].AddSlot(Slot.RightHand);
            testItemTypes[0].AddSlot(Slot.LeftHand);

            testItemTypes[1] = testGameObject.AddComponent<ItemType>();
            testItemTypes[1].Start();
            testItemTypes[1].SetTypeInfo(2, "Body Armor");
            testItemTypes[1].AddSlot(Slot.Chest);

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
            testItems[0].Type = testItemTypes[0];
            testItems[1].Type = testItemTypes[1];
		}

		[Test]
		public void TestEquipping()
		{
			Setup ();

            EquipmentManager equipMgr = testGameObject.GetComponent<EquipmentManager>();
            Inventory inventory = testGameObject.GetComponent<Inventory>();

            inventory.Add(testItems[0]);
            inventory.Add(testItems[0]); //2 of this item in inventory
            inventory.Add(testItems[1]);
            
			//Cannot equip a NULL item.
			Assert.False (equipMgr.Equip ((Item) null, Slot.Head));

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

		[Test]
		public void TestDequipping()
		{
			Setup ();

            EquipmentManager equipMgr = testGameObject.GetComponent<EquipmentManager>();
            Inventory inventory = testGameObject.GetComponent<Inventory>();

            inventory.Add(testItems[0]);
            inventory.Add(testItems[0]);
            inventory.Add(testItems[1]);

			Assert.True (equipMgr.Equip(testItems[0], Slot.RightHand));
			Assert.True (equipMgr.Equip(testItems[0], Slot.LeftHand));
			Assert.True (equipMgr.Equip(testItems[1], Slot.Chest));

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

 
    }
}
#endif
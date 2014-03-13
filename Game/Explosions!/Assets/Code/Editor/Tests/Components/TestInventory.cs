#if UNITY_EDITOR
using UnityEngine;
using System.Collections;
using NUnit.Framework;
using Assets.Code.Components;
using Assets.Code.Model;
using System.Collections.Generic;

namespace Assets.Code.Editor.Tests.Components
{
    [ExecuteInEditMode]
    class TestInventory
    {
		private GameObject testGameObj;
		private Item[] testItems;
		private Inventory testInventory;

		public void Setup()
		{
			testGameObj = new GameObject();
			testItems = new Item[4];
			testInventory = testGameObj.AddComponent<Inventory>();

			testInventory.Start ();

			testItems[0] = testGameObj.AddComponent<Item>();
			testItems[0].Awake ();
			testItems[0].name = "Item 01";

			testItems[1] = testGameObj.AddComponent<Item>();
			testItems[1].Awake ();
			testItems[1].name = "Item 02";

			testItems[2] = testGameObj.AddComponent<Item>();
			testItems[2].Awake ();
			testItems[2].name = "Item 02";
		}

		[Test]
		public void TestInventoryNullInput()
		{
			Setup ();

			Assert.False (testInventory.Contains ((Item) null));
			testInventory.Add ((Item) null);
			testInventory.Add ((Item) null);
			Assert.False (testInventory.Contains ((Item) null));
			Assert.False (testInventory.Remove ((Item) null));
			Assert.False (testInventory.Contains ((Item) null));
		}

		[Test]
		public void TestInventoryUsage()
		{
			Setup ();

			testInventory.Add (testItems[0]);
			testInventory.Add (testItems[1]);
			testInventory.Add (testItems[2]);

			Assert.True (testInventory.Contains (testItems[0]));
			Assert.True (testInventory.Contains (testItems[1]));
			Assert.True (testInventory.Contains (testItems[2]));

			Assert.True (testInventory.Remove (testItems[0]));
			Assert.True (testInventory.Remove (testItems[1]));
			Assert.True (testInventory.Remove (testItems[2]));

			testInventory.Add (testItems[0]);
			testInventory.Add (testItems[0]);

			Assert.True (testInventory.Contains (testItems[0]));
			Assert.False (testInventory.Contains(testItems[1]));
			Assert.False (testInventory.Contains(testItems[2]));

			Assert.True (testInventory.Remove (testItems[0]));
			Assert.True (testInventory.Contains(testItems[0]));
		}
    }
}
#endif
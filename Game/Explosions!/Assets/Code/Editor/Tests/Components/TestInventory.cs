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

        [SetUp]
		public void Setup()
		{
			testGameObj = new GameObject();
			testItems = new Item[4];
			testInventory = testGameObj.AddComponent<Inventory>();

			testInventory.Awake ();

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

        private Item CreateItem(string name)
        {
            GameObject go = new GameObject();
            Item ret = go.AddComponent<Item>();
            ret.Awake();
            ret.name = name;
            return ret;
        }

		[Test]
		public void TestInventoryNullInput()
		{
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

        public void TestGetQuantityZero()
        {
            Assert.AreEqual(0, testInventory.GetQuantity(testItems[0]));
        }

        public void TestGetQuantityDiffName()
        {
            testInventory.Add(testItems[0]);
            Item item1b = CreateItem("Item 011");
            Assert.AreEqual(0, testInventory.GetQuantity(item1b));
        }

        [Test]
        public void TestGetQuantitySameGo()
        {
            testInventory.Add(testItems[0]);
            Assert.AreEqual(1, testInventory.GetQuantity(testItems[0]));
        }

        [Test]
        public void TestGetQuantitySameName()
        {
            testInventory.Add(testItems[0]);
            Item item1b = CreateItem("Item 01");
            Assert.AreEqual(1, testInventory.GetQuantity(item1b));
        }

        [Test]
        public void TestAdd_SameGo()
        {
            testInventory.Add(testItems[0]);
            testInventory.Add(testItems[0]);
            Assert.AreEqual(2, testInventory.GetQuantity(testItems[0]));
        }

        [Test]
        public void TestAdd_SameName()
        {
            testInventory.Add(testItems[0]);
            Item item1b = CreateItem("Item 01");
            testInventory.Add(item1b);
            Assert.AreEqual(2, testInventory.GetQuantity(testItems[0]));
            Assert.AreEqual(2, testInventory.GetQuantity(item1b));
        }

        [Test]
        public void TestAdd_DiffName()
        {
            testInventory.Add(testItems[0]);
            Item item2 = CreateItem("Item 012");
            testInventory.Add(item2);
            Assert.AreEqual(1, testInventory.GetQuantity(testItems[0]));
            Assert.AreEqual(1, testInventory.GetQuantity(item2));
        }

        [Test]
        public void TestRemove_SameGo()
        {
            testInventory.Add(testItems[0]);
            Assert.True(testInventory.Remove(testItems[0]));
            Assert.AreEqual(0, testInventory.GetQuantity(testItems[0]));
        }

        [Test]
        public void TestRemove_SameName()
        {
            testInventory.Add(testItems[0]);
            Item item1b = CreateItem("Item 01");
            Assert.True(testInventory.Remove(item1b));
            Assert.AreEqual(0, testInventory.GetQuantity(testItems[0]));
            Assert.AreEqual(0, testInventory.GetQuantity(item1b));
        }

        [Test]
        public void TestRemove_DiffName()
        {
            testInventory.Add(testItems[0]);
            Item item2 = CreateItem("Item 02");
            Assert.False(testInventory.Remove(item2));
            Assert.AreEqual(1, testInventory.GetQuantity(testItems[0]));
            Assert.AreEqual(0, testInventory.GetQuantity(item2));
        }

        [Test]
        public void TestRemove_Negative()
        {
            testInventory.Add(testItems[0]);
            testInventory.Remove(testItems[0]);
            Assert.AreEqual(0, testInventory.GetQuantity(testItems[0]));
            testInventory.Remove(testItems[0]);
            Assert.AreEqual(0, testInventory.GetQuantity(testItems[0]));
        }
    }
}
#endif
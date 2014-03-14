#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using Assets.Code.Components;
using Assets.Code.Components.Actions;
using NUnit.Framework;
using UnityEngine;
using System.Linq;

namespace Assets.Code.Editor.Tests.Components
{
    [ExecuteInEditMode]
    class TestItem
    {
        private const string ITEM_0 = "Item0";
        private GameObject testGo;

        [SetUp]
        public void Setup()
        {
            testGo = Create(ITEM_0);
        }

        private GameObject Create(string name, bool awakeNow = false)
        {
            GameObject go = new GameObject();
            Item item = go.AddComponent<Item>();
            item.name = name;
            return go;
        }

        private Item CreateItem(string name, bool awakeNow = false)
        {
            return Create(name).GetComponent<Item>();
        }

        /// <summary>
        /// Unity calls start after all components have been added
        /// </summary>
        //TODO: Put this in a common location
        private void Init()
        {
            //Need delayed awake so we can actually see the attached actions
            testGo.GetComponent<Item>().Awake(); 

            //TODO: Try to get something like this working
            //var components = testGameObject.GetComponents<MonoBehaviour>();
            ////Awake is called before any Start has been called
            //foreach (MonoBehaviour mb in components)
            //{
            //    mb.Invoke("Awake", 0);
            //}
            //foreach (MonoBehaviour mb in components)
            //{
            //    mb.Invoke("Start", 0);
            //}
        }

        [TearDown]
        public void Cleanup()
        {
            //TODO:?
        }

        [Test]
        public void TestComparisonSameGo()
        {
            Init();
            Item item = testGo.GetComponent<Item>();
            Assert.AreEqual(0, item.CompareTo(item));
            Assert.True(item.Equals(item));
            Assert.AreEqual(item.GetHashCode(), item.GetHashCode());
        }

        [Test]
        public void TestComparisonSameName()
        {
            Init();
            Item item1 = testGo.GetComponent<Item>();
            Item item2 = CreateItem(ITEM_0, true);
            Assert.AreEqual(0, item1.CompareTo(item2));
            Assert.True(item1.Equals(item2));
            Assert.AreEqual(item1.GetHashCode(), item2.GetHashCode());
        }

        [Test]
        public void TestComparisonDifferent()
        {
            Init();
            Item item1 = testGo.GetComponent<Item>();
            Item item2 = CreateItem("Item1", true);
            Assert.AreNotEqual(0, item1.CompareTo(item2));
            Assert.False(item1.Equals(item2));
            Assert.AreNotEqual(item1.GetHashCode(), item2.GetHashCode());
        }

        [Test]
        public void TestComparisonNullGo()
        {
            Init();
            Item item1 = testGo.GetComponent<Item>();
            Item item2 = null;
            Assert.AreEqual(1, item1.CompareTo(item2));
            Assert.False(item1.Equals(item2));
        }

        [Test]
        public void TestComparisonNullName1()
        {
            Init();
            Item item1 = testGo.GetComponent<Item>();
            item1.name = null;
            Item item2 = CreateItem("Item1", true);
            Assert.AreEqual(-1, item1.CompareTo(item2));
            Assert.False(item1.Equals(item2));
            Assert.AreNotEqual(item1.GetHashCode(), item2.GetHashCode());
        }

        [Test]
        public void TestComparisonNullName2()
        {
            Init();
            Item item1 = testGo.GetComponent<Item>();
            Item item2 = CreateItem(null, true);
            Assert.AreEqual(1, item1.CompareTo(item2));
            Assert.False(item1.Equals(item2));
            Assert.AreNotEqual(item1.GetHashCode(), item2.GetHashCode());
        }

        [Test]
        public void TestNoActions()
        {
            Init();

            Item item = testGo.GetComponent<Item>();
            Assert.AreEqual(0, item.Actions.Count<GameAction>());
        }

        [Test]
        public void TestOneAction()
        {
            testGo.AddComponent<RayAttack>();
            Init();

            Item item = testGo.GetComponent<Item>();
            Assert.AreEqual(1, item.Actions.Count<GameAction>());
        }

        [Test]
        public void TestMultiAction()
        {
            //TODO: Test different kinds of actions when they exist
            testGo.AddComponent<RayAttack>();
            testGo.AddComponent<RayAttack>();
            testGo.AddComponent<RayAttack>();
            Init();

            Item item = testGo.GetComponent<Item>();
            Assert.AreEqual(3, item.Actions.Count<GameAction>());
        }
    }
}
#endif
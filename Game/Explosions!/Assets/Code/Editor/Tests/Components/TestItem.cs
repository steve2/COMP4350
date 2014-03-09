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
        private GameObject testGameObject;

        [SetUp]
        public void Setup()
        {
            testGameObject = new GameObject();
            testGameObject.AddComponent<Item>();
        }

        /// <summary>
        /// Unity calls start after all components have been added
        /// </summary>
        //TODO: Put this in a common location
        private void Init()
        {
            testGameObject.GetComponent<Item>().Awake();

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
        public void TestNoActions()
        {
            Init();

            Item item = testGameObject.GetComponent<Item>();
            Assert.AreEqual(0, item.Actions.Count<GameAction>());
        }

        [Test]
        public void TestOneAction()
        {
            testGameObject.AddComponent<RayAttack>();
            Init();

            Item item = testGameObject.GetComponent<Item>();
            Assert.AreEqual(1, item.Actions.Count<GameAction>());
        }

        [Test]
        public void TestMultiAction()
        {
            //TODO: Test different kinds of actions when they exist
            testGameObject.AddComponent<RayAttack>();
            testGameObject.AddComponent<RayAttack>();
            testGameObject.AddComponent<RayAttack>();
            Init();

            Item item = testGameObject.GetComponent<Item>();
            Assert.AreEqual(3, item.Actions.Count<GameAction>());
        }
    }
}
#endif
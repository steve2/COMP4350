#if UNITY_EDITOR
using UnityEngine;
using NUnit.Framework;
using Assets.Code.Components.Actions;
using System.Collections.Generic;
using Assets.Code.Editor.Stubs;

namespace Assets.Code.Editor.Tests.Components
{
    [ExecuteInEditMode]
    class TestGameActionManager
    {
        private GameObject testGO;

        [SetUp]
        public void Setup()
        {
            testGO = new GameObject();
            testGO.AddComponent<GameActionManager>();
        }

        public void Start()
        {
            GameActionManager actionMgr = testGO.GetComponent<GameActionManager>();
            actionMgr.Awake();
            actionMgr.Start();
        }

        private void PreloadAction(string name)
        {
            GameActionStub added = testGO.AddComponent<GameActionStub>();
            added.Start();
            added.SetName(name);
        }

        private GameActionStub CreateAction(string name)
        {
            GameObject temp = new GameObject();
            temp.AddComponent<GameActionStub>();
            GameActionStub ret = temp.GetComponent<GameActionStub>(); 
            ret.Start();
            ret.SetName(name);
            return ret;
        }

        [Test]
        public void TestAutoAddOne()
        {
            PreloadAction("test");
            Start();
            GameActionManager mgr = testGO.GetComponent<GameActionManager>();

            Assert.AreEqual(1, mgr.Count);
        }

        [Test]
        public void TestAutoAddMulti()
        {
            PreloadAction("test1");
            PreloadAction("test2");
            PreloadAction("test3");
            Start();
            GameActionManager mgr = testGO.GetComponent<GameActionManager>();

            Assert.AreEqual(3, mgr.Count);
        }

        [Test]
        public void TestAddOneAction()
        {
            Start();
            string name = "test";
            GameActionManager mgr = testGO.GetComponent<GameActionManager>();
            GameActionStub action = CreateAction(name);
            Assert.AreEqual(0, mgr.Count);
            mgr.AddAction(action);
            Assert.AreEqual(1, mgr.Count);
        }

        [Test]
        public void TestRemoveOneAction()
        {
            Start();
            string name = "test";
            GameActionManager mgr = testGO.GetComponent<GameActionManager>();
            GameActionStub action = CreateAction(name);
            Assert.AreEqual(0, mgr.Count);
            mgr.AddAction(action);
            Assert.AreEqual(1, mgr.Count);
            mgr.RemoveAction(action);
            Assert.AreEqual(0, mgr.Count);

        }

        [Test]
        public void TestOnePerform()
        {
            Start();
            string name = "test";
            GameActionManager mgr = testGO.GetComponent<GameActionManager>();
            GameActionStub action = CreateAction(name);
            mgr.AddAction(action);
            Assert.True(mgr.Perform(name));
            Assert.True(action.performed);
        }

        [Test]
        public void TestMultiPerform()
        {
            Start();
            string name1 = "test1";
            string name2 = "test2";
            string name3 = "test3";
            GameActionManager mgr = testGO.GetComponent<GameActionManager>();
            GameActionStub action1 = CreateAction(name1);
            GameActionStub action2 = CreateAction(name2);
            GameActionStub action3 = CreateAction(name3);
            mgr.AddAction(action1);
            mgr.AddAction(action2);
            mgr.AddAction(action3);
            Assert.True(mgr.Perform(name1));
            Assert.True(action1.performed);
            Assert.False(action2.performed);
            Assert.False(action3.performed);

            Assert.True(mgr.Perform(name3));
            Assert.True(action1.performed);
            Assert.False(action2.performed);
            Assert.True(action3.performed);

            Assert.True(mgr.Perform(name2));
            Assert.True(action1.performed);
            Assert.True(action2.performed);
            Assert.True(action3.performed);
        }

        [Test]
        public void TestInvalidPerform()
        {
            Start();
            string name = "test";
            GameActionManager mgr = testGO.GetComponent<GameActionManager>();
            GameActionStub action = CreateAction(name);
            mgr.AddAction(action);
            //Should fail gracefully (return false)
            Assert.False(mgr.Perform("FAIL"));
            Assert.False(action.performed);
        }

        //TODO: Test unique names
    }
}
#endif
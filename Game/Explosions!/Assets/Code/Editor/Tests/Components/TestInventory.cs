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

		public void Setup()
		{
			testGameObj = new GameObject();
			testItems = new Item[4];

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
    }
}
#endif
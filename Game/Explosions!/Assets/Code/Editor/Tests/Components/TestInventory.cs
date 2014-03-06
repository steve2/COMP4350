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
			testItems[0].Start ();
			testItems[0].name = "Item 01";

			testItems[1] = testGameObj.AddComponent<Item>();
			testItems[1].Start ();
			testItems[1].name = "Item 02";

			testItems[2] = testGameObj.AddComponent<Item>();
			testItems[2].Start ();
			testItems[2].name = "Item 02";
		}
    }
}
#endif
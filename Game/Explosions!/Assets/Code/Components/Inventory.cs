using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Assets.Code.Components
{
    public class Inventory : MonoBehaviour
    {
        private List<Item> items;

        public void Start()
        {
            items = new List<Item>();
        }

        public bool Contains(Item item)
        {
            return items.Contains(item);
        }

        public bool Remove(Item item)
        {
            return items.Remove(item);
        } 

        public void Add(Item item)
        {
            items.Add(item);
        }

		public void Print()
		{
			string toPrint = "";
			foreach (Item item in items)
			{
				toPrint += item.name + " | ";
			}
			Debug.Log (toPrint);
		}
    }
}
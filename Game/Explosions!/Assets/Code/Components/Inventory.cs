using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Assets.Code.Components
{
    public class Inventory : MonoBehaviour
    {
        [SerializeField]
        private List<Item> items;

        public void Start()
        {
            items = new List<Item>();
        }

        public bool Contains(Item item)
        {
			if (item != null)
			{
            	return items.Contains(item);
			}
			else
			{
				return false;
			}

        }

        public bool Remove(Item item)
        {
            if (item != null) 
			{
				return items.Remove(item);
			}
			else
			{
				return false;
			}
        } 

        public void Add(Item item)
        {
			if (item != null) 
			{
				items.Add (item);
			}
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
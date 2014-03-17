using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Assets.Code.Components
{
    public class Inventory : MonoBehaviour, IEnumerable<Item>
    {
        [SerializeField]
        private List<Item> editorItems;
        private Dictionary<Item, int> items;
      
        public void Start()
        {
            items = new Dictionary<Item, int>();
            if (editorItems != null)
            {
                foreach (Item item in editorItems)
                {
                    items.Add(item, 1);
                }
            }
        }

        public int GetQuantity(Item item)
        {
            int quantity = 0;

			if (item != null)
			{
                items.TryGetValue(item, out quantity);
            }

            return quantity;
        }

        public bool Contains(Item item)
        {
                return GetQuantity(item) > 0;
        }

        public bool Remove(Item item)
        {
            return Remove(item, 1);
        }
        public bool Remove(Item item, int quantity)
        {
            bool success = false;
            int current;
            if (item != null) 
			{
                if (items.TryGetValue(item, out current) && current > 0)
                {
                    items[item] =  current - 1;
                    success = true;
                }
			}

            return success;
        }

        public void Add(Item item)
        {
            Add(item, 1);
        }
        public void Add(Item item, int quantity)
        {
            int current;
			if (item != null) 
			{
                if (items.TryGetValue(item, out current))
                {
                    items[item] = current + quantity;
                }
                else
                {
                    items.Add(item, quantity);
                }
			}
        }

		public void Print()
		{
			string toPrint = "";
			foreach (KeyValuePair<Item, int> entry in items)
			{
				toPrint += entry.Key.Name + " ("+ entry.Value +"), ";
			}
			Debug.Log (toPrint);
		}

        public IEnumerator<Item> GetEnumerator()
        {
            return items.Keys.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
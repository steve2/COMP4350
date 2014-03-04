using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Assets.Code.Components
{
    public class Inventory : MonoBehaviour
    {
        private HashSet<Item> items;

        public void Start()
        {
            items = new HashSet<Item>();
        }

        public bool Contains(Item item)
        {
            return items.Contains(item);
        }

        public bool Remove(Item item)
        {
            return items.Remove(item);
        }

        public bool Add(Item item)
        {
            return items.Add(item);
        }
    }
}
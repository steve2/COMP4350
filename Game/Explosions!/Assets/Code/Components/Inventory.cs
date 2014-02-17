using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Assets.Code.Components
{
    [RequireComponent(typeof(AttributeManager))]
    public class Inventory : MonoBehaviour
    {
        private AttributeManager attrMgr;
        private HashSet<Item> items;

        // Use this for initialization
        public void Start()
        {
            attrMgr = GetComponent<AttributeManager>();
            items = new HashSet<Item>();
        }

        public void Equip(Item item)
        {
            items.Add(item);
            attrMgr.AddAttributes(item.ItemAttributes);
        }

        public void Dequip(Item item)
        {
            if (items.Contains(item))
            {
                items.Remove(item);
                attrMgr.SubtractAttributes(item.ItemAttributes);
            }
        }
    }
}
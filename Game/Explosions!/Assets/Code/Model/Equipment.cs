using UnityEngine;
using System;
using Assets.Code.Model;
using System.Collections;
using System.Collections.Generic;

namespace Assets.Code.Components
{
    public class Equipment : IEnumerable<KeyValuePair<Slot, Item>>
    {
        /** Constant Definitions **/
        public static int EQUIP_SLOTS = Enum.GetValues(typeof(Slot)).Length;

        /** Class Members **/
        private Dictionary<Slot, Item> equippedItems;

        /** Initialization **/
        public Equipment()
        {
            equippedItems = new Dictionary<Slot, Item>(EQUIP_SLOTS);
        }

		public void Print()
		{
			string toPrint = "";
			foreach (KeyValuePair<Slot, Item> entry in equippedItems)
			{
				toPrint += entry.Value.Name + " (" + entry.Key + "), ";
			}
			Debug.Log (toPrint);
		}

        /** Get/Set Slot Item **/
        public Item GetSlot(Slot whichSlot)
        {
            Item ret = null;
            equippedItems.TryGetValue(whichSlot, out ret);
            return ret;
        }

        public void SetSlot(Slot whichSlot, Item setTo)
        {
            if (equippedItems.ContainsKey(whichSlot))
            {
                equippedItems[whichSlot] = setTo;
            }
            else
            {
                equippedItems.Add(whichSlot, setTo);
            }
        }

        public void ClearSlot(Slot whichSlot)
        {
            if (equippedItems.ContainsKey(whichSlot))
            {
                equippedItems.Remove(whichSlot);
            }
        }

        public IEnumerator<KeyValuePair<Slot, Item>> GetEnumerator()
        {
            return equippedItems.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return equippedItems.GetEnumerator();
        }
    }
}


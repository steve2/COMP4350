using UnityEngine;
using System;
using Assets.Code.Model;
using System.Collections;
using System.Collections.Generic;

namespace Assets.Code.Components
{
    public class Equipment
    {
        /** Constant Definitions **/
        public static int EQUIP_SLOTS = Enum.GetValues(typeof(Slot)).Length;

        /** Class Members **/
        private Item[] equippedItems; //initialized to NULL entries

        /** Initialization **/
        public Equipment()
        {
            equippedItems = new Item[EQUIP_SLOTS];
            for (int i = 0; i < EQUIP_SLOTS; i++)
            {
                equippedItems[i] = null;
            }
        }

        /** Get/Set Slot Item **/
        public Item GetSlot(Slot whichSlot)
        {
			return equippedItems[(int) whichSlot - 1];	
        }

        public void SetSlot(Slot whichSlot, Item setTo)
        {
			equippedItems[(int)whichSlot - 1] = setTo;
        }
    }
}


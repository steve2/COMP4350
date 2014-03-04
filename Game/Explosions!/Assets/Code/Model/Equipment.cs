using UnityEngine;
using System;
using Assets.Code.Model;
using System.Collections;
using System.Collections.Generic;

namespace Assets.Code.Components
{
    public class Equipment : MonoBehaviour
    {
        /** Constant Definitions **/
        public static int EQUIP_SLOTS = Enum.GetValues(typeof(Slot)).Length;

        /** Class Members **/
        private Item[] equippedItems; //initialized to NULL entries

        /** In Bounds Check **/
        private bool IndexInBounds(int index)
        {
            return (index >= 0 && index < EQUIP_SLOTS);
        }

        /** Initialization **/
        public void Start()
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
            if (IndexInBounds((int) whichSlot - 1))
            {
                return equippedItems[(int) whichSlot - 1];
            }
            return null;
        }

        public bool SetSlot(Slot whichSlot, Item setTo)
        {
            if (IndexInBounds((int) whichSlot - 1))
            {

                equippedItems[(int) whichSlot - 1] = setTo;
                return true;
            }
            return false;
        }
    }
}
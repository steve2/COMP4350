using UnityEngine;
using System.Collections;
using Assets.Code.Model;
using System.Collections.Generic;

namespace Assets.Code.Components
{
    [RequireComponent(typeof(AttributeManager))]
    [RequireComponent(typeof(Inventory))]
    public class EquipmentManager : MonoBehaviour
    {
        private AttributeManager attributeMngr;
        private Equipment equipment;
        private Inventory inventory;

        /** Initialization **/
        public void Start()
        {
            equipment = new Equipment();
            inventory = GetComponent<Inventory>();
            attributeMngr = GetComponent<AttributeManager>();
        }

        /**
         * bool Dequip ()
         *  @ whatSlot: What slot to unequip from?
         *  
         * Returns TRUE if successful (Item in "whatSlot" should be in Inventory). 
         * Returns FALSE if there is not Item in "whatSlot", or if "whatSlot" doesn't exist.
         * 
         * Moves the Item in "whatSlot" from EQUIPMENT to INVENTORY.
         * This will set the contents of "whatSlot" to NULL.
         */
        public bool Dequip(Slot whatSlot)
        {
            Item equipped = equipment.GetSlot(whatSlot);
            if (equipped != null)
            {
				inventory.Add(equipped);
				equipment.SetSlot(whatSlot, null);
                attributeMngr.SubtractAttributes(equipped);
                return true;
            }
            return false;
        }

        /**
         * bool Equip ()
         *  @ toEquip: Item to equip.
         *  @ whatSlot: What slot to equip to?
         *  
         * Returns TRUE if the Equip was successful, and FALSE if the Item
         * could not be placed in the desired Slot (or the Item is not in Inventory).
         * 
         * The specified Slot "whatSlot" is Dequipped from prior to Equipping
         * the new Item. This will essentially "swap" the Equipped item in that spot.
         * 
         * It is the responsibility of this manager to check to make sure that an Item
         * does not violate any "Slot" requirements.
         */
        public bool Equip(Item toEquip, Slot whatSlot)
        {
            if (toEquip == null || !inventory.Contains(toEquip))
            {
                return false;
            }
			if (toEquip.GetItemType().IsSlotAllowed (whatSlot))
			{
            	Dequip(whatSlot);
				equipment.SetSlot(whatSlot, toEquip);
				inventory.Remove (toEquip);
				attributeMngr.AddAttributes(toEquip);
				return true;
			}
			return false;
        }

    }
}

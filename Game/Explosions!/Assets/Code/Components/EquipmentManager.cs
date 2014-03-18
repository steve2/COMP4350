using UnityEngine;
using System.Collections;
using Assets.Code.Model;
using System.Collections.Generic;
using Assets.Code.Components.Actions;
using Assets.Code.Controller;

namespace Assets.Code.Components
{
    [RequireComponent(typeof(AttributeManager))]
    [RequireComponent(typeof(GameActionManager))]
    [RequireComponent(typeof(Inventory))]
    public class EquipmentManager : MonoBehaviour, IEnumerable<KeyValuePair<Slot, Item>>
    {
        private AttributeManager attributeMngr;
        private GameActionManager actionMgr;
        private Equipment equipment;
		private ItemTypeSlotChecker slotPermissions;
        private Inventory inventory;

        /** Initialization **/
        public void Awake()
        {
            equipment = new Equipment();
            inventory = GetComponent<Inventory>();
            attributeMngr = GetComponent<AttributeManager>();
            actionMgr = GetComponent<GameActionManager>();

			slotPermissions = new ItemTypeSlotChecker();
			slotPermissions.AddSlotToType(ItemType.Weapon, Slot.RightHand);
			slotPermissions.AddSlotToType(ItemType.Weapon, Slot.LeftHand);
			slotPermissions.AddSlotToType(ItemType.Chest, Slot.Chest);
			slotPermissions.AddSlotToType(ItemType.Legs, Slot.Legs);
        }

		public Inventory GetInventory()
		{
			return inventory;
		}

		public void SetInventory(Inventory set)
		{
			inventory = set;
		}

		public void Print()
		{
			equipment.Print ();
		}

        public List<Slot> GetSlots(ItemType type)
        {
            return slotPermissions.GetSlots(type);
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
				equipment.ClearSlot(whatSlot);
                attributeMngr.SubtractAttributes(equipped);
                actionMgr.RemoveActions(equipped.Actions);
                DestroyPrefab(equipped);
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
            if (toEquip == null || !inventory.Contains(toEquip)) //TODO: THIS WILL BREAK
            {
                return false;
            }
			if (slotPermissions.CheckSlotPermission(toEquip.Type, whatSlot)) //TODO: THIS WILL BREAK
			{
                Dequip(whatSlot);
                Item actualItem = InitPrefab(toEquip);
                inventory.Remove(actualItem);
                equipment.SetSlot(whatSlot, actualItem);
				attributeMngr.AddAttributes(actualItem);
                actionMgr.AddActions(actualItem.Actions);
				return true;
			}
			return false;
        }

        protected virtual Item InitPrefab(Item prefab)
        {
            Item instance = (Item)Instantiate(prefab, transform.position, transform.rotation);
            instance.transform.parent = this.transform;
            return instance;
        }

        protected virtual void DestroyPrefab(Item instance)
        {
            Destroy(instance);
        }

        IEnumerator<KeyValuePair<Slot, Item>> IEnumerable<KeyValuePair<Slot, Item>>.GetEnumerator()
        {
            return equipment.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return equipment.GetEnumerator();
        }
    }
}

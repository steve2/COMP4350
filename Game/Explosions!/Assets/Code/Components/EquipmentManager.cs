using UnityEngine;
using System.Collections;
using Assets.Code.Model;
using System.Collections.Generic;
using Assets.Code.Components.Actions;

namespace Assets.Code.Components
{
    [RequireComponent(typeof(AttributeManager))]
    [RequireComponent(typeof(GameActionManager))]
    [RequireComponent(typeof(Inventory))]
    public class EquipmentManager : MonoBehaviour
    {
        private AttributeManager attributeMngr;
        private GameActionManager actionMgr;
        private Equipment equipment;
        [SerializeField]
        private List<Item> defaultEquipment; //Loaded from the editor
        private Inventory inventory;

        /** Initialization **/
        public void Start()
        {
            equipment = new Equipment();
            inventory = GetComponent<Inventory>();
            attributeMngr = GetComponent<AttributeManager>();
            actionMgr = GetComponent<GameActionManager>();

			Invoke ("DefaultEquipmentHACK", 0.5f);
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
			if (toEquip.Type.IsSlotAllowed (whatSlot)) //TODO: THIS WILL BREAK
			{
            	Dequip(whatSlot);
                Item actualItem = InitPrefab(toEquip);
				equipment.SetSlot(whatSlot, actualItem);
				inventory.Remove (actualItem);
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

		private void DefaultEquipmentHACK()
		{
			//HACK: REMOVE THIS when loading is complete (Use the injected server instead)
			//Load in some default items from editor without contacting server (Ex: Demo)
			if (defaultEquipment != null)
			{
				foreach(Item item in defaultEquipment)
				{
                    Item actualItem = InitPrefab(item);
                    attributeMngr.AddAttributes(actualItem);
					actionMgr.AddActions(actualItem.Actions);
				}
			}
		}
	}
}

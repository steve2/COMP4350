using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Code.Model;
using UnityEngine;


namespace Assets.Code.Controller
{

	public class ItemTypeSlotChecker
    {
		private static ItemTypeSlotChecker singleton;
		private List<Slot>[] slotPermissionsArray;

        public ItemTypeSlotChecker()
        {
			slotPermissionsArray = new List<Slot>[Enum.GetValues(typeof(ItemType)).Length];
			for (int i = 0; i < slotPermissionsArray.Length; i++)
			{
				slotPermissionsArray[i] = new List<Slot>();
			}
        }
        
        //TODO: Try to make this public interface IEnumerable<Slot>
        public List<Slot> GetSlots(ItemType type)
        {
            return Get(type);
        }

        private List<Slot> Get(ItemType type)
        {
            return slotPermissionsArray[(int)type - 1];
        }

		/**
		 * Adds a Slot permission to the necessary List.
		 *  -IMPORTANT: List index is based on the ENUM values
		 *  of the ItemType class, beginning at 1 and incrementing by 1.
		 *  
		 *  A switch would need to be implemented to do otherwise.
		 */
        public bool AddSlotToType(ItemType type, Slot add)
        {
			if (!Get(type).Contains (add))
			{
                Get(type).Add(add);
				return true;
			}
			return false;
        }

		public bool CheckSlotPermission(ItemType type, Slot check)
		{
			return Get(type).Contains (check);
		}
    }

}

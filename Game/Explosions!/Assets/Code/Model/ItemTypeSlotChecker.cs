using System;
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

		/**
		 * Adds a Slot permission to the necessary List.
		 *  -IMPORTANT: List index is based on the ENUM values
		 *  of the ItemType class, beginning at 1 and incrementing by 1.
		 *  
		 *  A switch would need to be implemented to do otherwise.
		 */
        public bool AddSlotToType(ItemType type, Slot add)
        {
			if (!slotPermissionsArray[(int) type - 1].Contains (add))
			{
				slotPermissionsArray[(int) type - 1].Add (add);
				return true;
			}
			return false;
        }

		public bool CheckSlotPermission(ItemType type, Slot check)
		{
			return slotPermissionsArray[(int) type - 1].Contains (check);
		}
    }

}

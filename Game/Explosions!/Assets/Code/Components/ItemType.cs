using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Code.Model;
using UnityEngine;


namespace Assets.Code.Model
{
	public class ItemType : MonoBehaviour
    {
        private int typeId;
        private string typeName;
        private List<Slot> allowedSlots;

        public virtual void Start()
        {
            allowedSlots = new List<Slot>();
        }

        public void SetTypeInfo(int id, string name)
        {
			this.typeId = id;
			this.typeName = name;
        }

		public string GetTypeName()
		{
			return typeName;
		}

		public int GetTypeId()
		{
			return typeId;
		}

        public void AddSlot(Slot allowed)
        {
            allowedSlots.Add(allowed);
        }

        public bool RemoveSlot(Slot allowed)
        {
            return allowedSlots.Remove(allowed);
        }

        public bool IsSlotAllowed(Slot check)
        {
            return allowedSlots.Contains(check);
        }
    }

}

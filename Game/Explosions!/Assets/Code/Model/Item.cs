using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Code.Model
{
    //Identical to database
    public enum ItemType
    {
        Weapon = 0,
    }

    /// <summary>
    /// An item is a changeable list of attributes
    /// Can be directly mapped from the Database
    /// </summary>
    [System.Serializable]
    public class Item
    {
        #region Fields
        private Dictionary<AttributeType, ItemAttribute> attributes; //Some redundant storage, but very quick attribute search (Sacrifice storage for speed) 
        #endregion

        public Item()
        {
            attributes = new Dictionary<AttributeType, ItemAttribute>();
        }

        #region Public Interface
        public ItemAttribute GetAttribute(AttributeType type)
        {
            if (attributes.ContainsKey(type))
            {
                return attributes[type];
            }

            return null;
        }

        public void AddAttribute(AttributeType type, int value)
        {
            AddAttribute(new ItemAttribute(type, value));
        }

        public void AddAttribute(ItemAttribute attr)
        {
            if (!attributes.ContainsKey(attr.Type))
            {
                attributes.Add(attr.Type, attr);
            }
        }

        public void RemoveAttribute(AttributeType type)
        {
            attributes.Remove(type);
        }
        #endregion
    }
}

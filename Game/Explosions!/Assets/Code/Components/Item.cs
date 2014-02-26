using System;
using System.Collections.Generic;
using Assets.Code.Model;
using UnityEngine;

namespace Assets.Code.Components
{
    /*
    // Different types of Items
    public enum ItemType
    {
        Weapon
    }
     */

    //TODO: Override Comparison to compare by name?
    public abstract class Item : MonoBehaviour
    {
        #region Editor Fields
        //Note: Use the built in name
        [SerializeField]
        private string description; 
        #endregion

        private List<ItemAttribute> itemAttributes; //The "REAL" attribute list

        #region Properties
        //All items support attributes
        public IEnumerable<ItemAttribute> ItemAttributes { get { return itemAttributes; } }
        #endregion

        // Use this for initialization
        public virtual void Start()
        {
            InitAttributes();
        }

        protected virtual void InitAttributes()
        {
            itemAttributes = new List<ItemAttribute>();
        }

        protected void AddAttribute(AttributeType type, int value)
        {
            AddAttribute(new ItemAttribute(type, value));
        }

        protected void AddAttribute(ItemAttribute attr)
        {
            itemAttributes.Add(attr);
        }
    }
}

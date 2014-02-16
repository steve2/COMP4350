using System;
using System.Collections.Generic;
using Assets.Code.Model;
using UnityEngine;

namespace Assets.Code.Components
{
    public abstract class Item : MonoBehaviour
    {
        private List<ItemAttribute> attributes; //The "REAL" attribute list

        #region Properties
        //All items support attributes
        public IEnumerable<ItemAttribute> ItemAttributes { get { return attributes; } }
        #endregion

        // Use this for initialization
        public virtual void Start()
        {
            InitAttributes();
        }

        protected virtual void InitAttributes()
        {
            attributes = new List<ItemAttribute>();
        }

        protected void AddAttribute(AttributeType type, int value)
        {
            AddAttribute(new ItemAttribute(type, value));
        }

        protected void AddAttribute(ItemAttribute attr)
        {
            attributes.Add(attr);
        }
    }
}

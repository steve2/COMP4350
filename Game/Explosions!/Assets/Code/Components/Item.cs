using System;
using System.Collections.Generic;
using Assets.Code.Model;
using UnityEngine;

namespace Assets.Code.Components
{
    //TODO: Override Comparison to compare by name?
    public abstract class Item : MonoBehaviour
    {
        #region Editor Fields
        //Note: Use the built in name
        [SerializeField]
        private string description; 
        #endregion

        private List<GameAttribute> itemAttributes; //The "REAL" attribute list

        #region Properties
        //All items support attributes
        public IEnumerable<GameAttribute> ItemAttributes { get { return itemAttributes; } }
        #endregion

        Attribute bla;
        // Use this for initialization
        public virtual void Start()
        {
            InitAttributes();
        }

        protected virtual void InitAttributes()
        {
            itemAttributes = new List<GameAttribute>();
        }

        protected void AddAttribute(AttributeType type, int value)
        {
            AddAttribute(new GameAttribute(type, value));
        }

        protected void AddAttribute(GameAttribute attr)
        {
            itemAttributes.Add(attr);
        }
    }
}

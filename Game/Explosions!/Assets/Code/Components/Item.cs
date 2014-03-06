using System;
using System.Collections.Generic;
using Assets.Code.Model;
using UnityEngine;

namespace Assets.Code.Components
{
    //TODO: Override Comparison to compare by name?
    public class Item : MonoBehaviour, IEnumerable<GameAttribute>  //All items support attributes
    {
		public string name;


        #region Editor Fields
        //Note: Use the built in name
        [SerializeField]
        private string description; 
        #endregion

        private List<GameAttribute> itemAttributes; //The "REAL" attribute list
		private ItemType itemtype;

        #region Properties
        #endregion

        // Use this for initialization
        public virtual void Start()
        {
            InitAttributes();   
        }

        protected virtual void InitAttributes()
        {
            itemAttributes = new List<GameAttribute>();
        }

		public void SetItemType(ItemType setTo)
		{
			itemtype = setTo;
		}

		public ItemType GetItemType()
		{
			return itemtype;
		}

        public void AddAttribute(AttributeType type, int value)
        {
            AddAttribute(new GameAttribute(type, value));
        }

        public void AddAttribute(GameAttribute attr)
        {
            itemAttributes.Add(attr);
        }

        public IEnumerator<GameAttribute> GetEnumerator()
        {
            return itemAttributes.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}

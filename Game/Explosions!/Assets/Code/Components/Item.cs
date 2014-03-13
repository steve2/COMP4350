using System;
using System.Collections.Generic;
using Assets.Code.Components.Actions;
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
        private IEnumerable<GameAction> actions; //The actions to perform when used
        [SerializeField]
		private ItemType itemtype;

        #region Properties
        public ItemType Type 
        { 
            get { return itemtype; } 
            set { itemtype = value; } 
        }
        public IEnumerable<GameAction> Actions { get { return actions; } }
        #endregion


        // Use this for initialization
        public virtual void Awake()
        {
            InitAttributes();
            InitActions();
        }

        protected virtual void InitAttributes()
        {
            itemAttributes = new List<GameAttribute>();
        }

        protected virtual void InitActions()
        {
            actions = GetComponents<GameAction>(); //Can have any number of actions (Including 0)
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

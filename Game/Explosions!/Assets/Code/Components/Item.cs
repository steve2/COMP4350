using System;
using System.Collections.Generic;
using Assets.Code.Components.Actions;
using Assets.Code.Model;
using UnityEngine;

namespace Assets.Code.Components
{
    //TODO: Override Comparison to compare by name?
    public class Item : MonoBehaviour, 
        IEnumerable<GameAttribute>, IComparable<Item>, IEquatable<Item> 
    {
		public string name; //TODO: Fields shouldn't be public

        #region Editor Fields
        //Note: Use the built in name
        [SerializeField]
        private string description;
        [SerializeField]
        private List<GameAttribute> itemAttributes; //The attribute list
        #endregion

        private IEnumerable<GameAction> actions; //The actions to perform when used
        [SerializeField]
		private ItemType itemtype;

        #region Properties
        public string Name { get { return name; } }
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

        public override string ToString()
        {
            return name; //TODO: Could put in more info here
        }

        public IEnumerator<GameAttribute> GetEnumerator()
        {
            return itemAttributes.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public override int GetHashCode()
        {
            if (name == null)
            {
                return 0;
            }
            else
            {
                return name.GetHashCode();
            }
        }

        //TODO: Check if name is null?
        public int CompareTo(Item other)
        {
            if (other == null || other.name == null)
            {
                return 1;
            }
            if (this.name == null)
            {
                return -1;
            }
            return this.name.CompareTo(other.name);
        }

        public bool Equals(Item other)
        {
            if (other == null)
            {
                return false;
            }

            return this.CompareTo(other) == 0;
        }
    }
}

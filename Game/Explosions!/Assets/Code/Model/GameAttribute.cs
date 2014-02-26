using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Code.Model
{
    //The different types of attributes
    public enum AttributeType
    {
        Damage,
        Health,
        Speed,
        Range,
        Capacity
    }

    /// <summary>
    /// An attribute is a name/value pair that has a meaning
    /// Can be directly mapped from the Database
    /// </summary>
    [System.Serializable]
    public class GameAttribute
    {
        #region Fields
        [SerializeField]
        private AttributeType type; //The type of attribute (In-game interpretation)
        [SerializeField]
        private int value; 
        #endregion

        #region Properties
        public AttributeType Type
        {
            get { return type; }
        }

        public int Value
        {
            get { return value; }
        }

        public string Name
        {
            get
            {
                //TODO: Derive name from type
                throw new NotImplementedException();
            }
        } 
        #endregion

        #region Constructor
        public GameAttribute(AttributeType type) :
            this(type, 0)
        { }

        public GameAttribute(AttributeType type, int value)
        {
            this.type = type;
            this.value = value;
        }
       
        #endregion
    }
}

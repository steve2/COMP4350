using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Code.Model
{

    //The diferent types of attributes
    public enum AttributeType
    {
        ItemType,
        Damage,
        Health
    }

    /// <summary>
    /// An attribute is a name/value pair that has a meaning
    /// Can be directly mapped from the Database
    /// </summary>
    [System.Serializable]
    public class ItemAttribute
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
        public ItemAttribute(AttributeType type, int value)
        {
            this.type = type;
            this.value = value;
        } 
        #endregion
    }
}

using System;
using UnityEngine;
using System.Collections;
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
    [Obsolete("No longer necessary", true)]
    public class ItemAttrCollection : ICollection<ItemAttribute>
    {
        #region Fields
        private Dictionary<AttributeType, ItemAttribute> attributes; //Some redundant storage, but very quick attribute search (Sacrifice storage for speed) 
        #endregion

        public int Count
        {
            get { return attributes.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public ItemAttrCollection()
        {
            attributes = new Dictionary<AttributeType, ItemAttribute>();
        }

        #region Public Interface
        //Custom
        public ItemAttribute Get(AttributeType type)
        {
            if (attributes.ContainsKey(type))
            {
                return attributes[type];
            }

            return null;
        }

        //Custom overload
        public void Add(AttributeType type, int value)
        {
            Add(new ItemAttribute(type, value));
        }

        public void Add(ItemAttribute attr)
        {
            if (!attributes.ContainsKey(attr.Type))
            {
                attributes.Add(attr.Type, attr);
            }
        }

        public void Clear()
        {
            attributes.Clear();
        }

        public bool Contains(ItemAttribute attr)
        {
            return attributes.ContainsKey(attr.Type);
        }

        public void CopyTo(ItemAttribute[] arr, int idx)
        {
            foreach (ItemAttribute attr in this)
            {
                arr[idx] = attr;
                idx++;
            }
        }

        public IEnumerator<ItemAttribute> GetEnumerator()
        {
            return attributes.Values.GetEnumerator();
        }
        
        IEnumerator IEnumerable.GetEnumerator()
        {
            return attributes.Values.GetEnumerator();
        }
        
        public bool Remove(ItemAttribute item)
        {
            return Remove(item.Type);
        }

        //Custom overload
        public bool Remove(AttributeType type)
        {
            return attributes.Remove(type);
        }
        #endregion


        //System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        //{
        //    throw new NotImplementedException();
        //}



    }
}

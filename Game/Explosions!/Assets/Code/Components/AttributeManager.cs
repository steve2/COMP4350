﻿using UnityEngine;
using System.Collections;
using Assets.Code.Model;
using System.Collections.Generic;

namespace Assets.Code.Components
{
    public class AttributeManager : MonoBehaviour
    {
        private Dictionary<AttributeType, int> attributes; //Theoretically O(1) lookup

        // Use this for initialization
        public void Start()
        {
            attributes = new Dictionary<AttributeType, int>();
        }

        public void AddAttributes(IEnumerable<ItemAttribute> attrs)
        {
            int value;
            foreach (ItemAttribute attr in attrs)
            {
                if (attributes.TryGetValue(attr.Type, out value))
                {
                    attributes[attr.Type] = value + attr.Value;
                }
                else
                {
                    attributes.Add(attr.Type, attr.Value);
                }
            }
        }

        //This should not be used to create a new attribute with negative values, use AddAttributes for that
        public void SubtractAttributes(IEnumerable<ItemAttribute> attrs)
        {
            int value;
            foreach (ItemAttribute attr in attrs)
            {
                if (attributes.TryGetValue(attr.Type, out value))
                {
                    attributes[attr.Type] = value - attr.Value;
                }
            }
        }

        public int GetAttributeValue(AttributeType type)
        {
            int value;
            if (!attributes.TryGetValue(type, out value))
                value = 0;

            return value;
        }
    }
}

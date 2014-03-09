using UnityEngine;
using System.Collections;
using Assets.Code.Model;
using System.Collections.Generic;

namespace Assets.Code.Components
{
    public class AttributeManager : MonoBehaviour
    {
        [SerializeField]
        private List<GameAttribute> baseAttributes; //This objects base attributes (Before anything else is applied)
        private Dictionary<AttributeType, int> attributes; //Theoretically O(1) lookup

        // Use this for initialization
        public void Start()
        {
            attributes = new Dictionary<AttributeType, int>();
            if (baseAttributes != null)
            {
                AddAttributes(baseAttributes);
            }
        }

        public void AddAttributes(IEnumerable<GameAttribute> attrs)
        {
            int value;
            foreach (GameAttribute attr in attrs)
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
        public void SubtractAttributes(IEnumerable<GameAttribute> attrs)
        {
            int value;
            foreach (GameAttribute attr in attrs)
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

using System;
using UnityEngine;
using System.Collections;
using Assets.Code.Interfaces;
using Assets.Code.Model;
using System.Collections.Generic;

namespace Assets.Code.Components
{
    [AddComponentMenu("Items/Weapon")]
    public class Weapon : Item
    {
        #region Editor Fields
        [SerializeField]
        private Weapon.EditorAttributes attributes; //The Editor attributes
        [Serializable]
        public class EditorAttributes
        {
            //Standard weapon fields
            public int damage;
            public int speed;
            public int range;
            public int capacity; //TODO: Ranged only? Or can we interpret capacity for a melee weapon?
            public List<GameAttribute> extraAttributes; //Extra attributes managed by the inspector
            
            public void Clear()
            {
                extraAttributes.Clear();
            }
        }
        #endregion

        #region Fields
        private AttributeManager attrManager; //Requires a link to owners attribute manager 
        #endregion
        
        //For testing (TODO: Shouldn't expose a public constructor)
        //This is used to simulate Unity initializing the data from inspector input
        public Weapon(Weapon.EditorAttributes editorAttributes) 
        {
            this.attributes = editorAttributes;
        }

        #region Overrides
        protected override void InitAttributes()
        {
            base.InitAttributes();
            //TODO: Change how ItemType is stored (It's one of the top level Item properties)
            //AddAttribute(AttributeType.ItemType, (int)ItemType.Weapon);
            AddAttribute(AttributeType.Damage, attributes.damage);
            AddAttribute(AttributeType.Speed, attributes.speed);
            AddAttribute(AttributeType.Range, attributes.range);
            AddAttribute(AttributeType.Capacity, attributes.capacity);
            foreach (GameAttribute attr in attributes.extraAttributes)
            {
                AddAttribute(attr);
            }

            attributes.Clear();
            attributes = null; //"Free"

            //TODO: Set item type to weapon
        }
        #endregion
    }
}

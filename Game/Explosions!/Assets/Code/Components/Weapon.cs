using System;
using UnityEngine;
using System.Collections;
using Assets.Code.Interfaces;
using Assets.Code.Model;
using System.Collections.Generic;

namespace Assets.Code.Components
{
    [AddComponentMenu("Items/Weapon")]
    //TODO: Should create child components MeleeWeapon/RangedWeapon (And make this abstract)
    public class Weapon : MonoBehaviour, IUsableItem
    {
        #region Editor Fields
        [SerializeField]
        private Weapon.Attributes attributes; //The Editor attributes
        [Serializable]
        public class Attributes
        {
            //Standard weapon fields
            public int damage;
            public List<ItemAttribute> extraAttributes; //Extra attributes managed by the inspector
            
            public void Clear()
            {
                extraAttributes.Clear();
            }
        }
        #endregion

        #region Fields
        private List<ItemAttribute> _attributes; //The "REAL" attribute list
        private AttributeManager attrManager; //Requires a link to owners attribute manager 
        #endregion

        #region Properties
        public IEnumerable<ItemAttribute> ItemAttributes { get { return _attributes; } } 
        #endregion

        //For testing (TODO: Shouldn't expose a public constructor)
        //This is used to simulate Unity initializing the data from inspector input
        public Weapon(Weapon.Attributes editorAttributes) 
        {
            this.attributes = editorAttributes;
        }

        #region Public Interface
        public bool Use()
        {
            //TODO: Implement
            throw new NotImplementedException();
        } 
        #endregion

        #region Unity Methods
        // Use this for initialization
        public void Start()
        {
            InitItem();
        }

        // Update is called once per frame
        void Update()
        {

        } 
        #endregion

        #region Private Methods
        private void InitItem()
        {
            _attributes = new List<ItemAttribute>();
            _attributes.Add(new ItemAttribute(AttributeType.ItemType, (int)ItemType.Weapon));
            _attributes.Add(new ItemAttribute(AttributeType.Damage, attributes.damage));
            foreach (ItemAttribute attr in attributes.extraAttributes)
            {
                _attributes.Add(attr);
            }

            attributes.Clear();
            attributes = null; //"Free"
        }
        #endregion
    }
}

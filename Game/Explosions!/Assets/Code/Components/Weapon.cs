﻿using System;
using UnityEngine;
using System.Collections;
using Assets.Code.Interfaces;
using Assets.Code.Model;
using System.Collections.Generic;

namespace Assets.Code.Components
{
    [AddComponentMenu("Items/Weapon")]
    //TODO: Should create child components MeleeWeapon/RangedWeapon (And make this abstract)
    class Weapon : MonoBehaviour, IUsableItem
    {
        #region Editor Fields
        //Standard weapon fields
        [SerializeField]
        private int damage;
        [SerializeField] //Assigned by Inspector (Ignore warning)
        private List<ItemAttribute> extraAttributes; //Extra attributes managed by the inspector, we need this because the inspector does not support dictionaries
        #endregion

        #region Fields
        private Item item;
        private AttributeManager attrManager; //Requires a link to owners attribute manager 
        #endregion
        
        #region Public Interface
        public bool Use()
        {
            //TODO: Implement
            throw new NotImplementedException();
        } 
        #endregion

        #region Unity Methods
        // Use this for initialization
        void Start()
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
            item = new Item();
            item.AddAttribute(AttributeType.ItemType, (int)ItemType.Weapon);
            item.AddAttribute(AttributeType.Damage, damage);
            foreach (ItemAttribute attr in extraAttributes)
            {
                item.AddAttribute(attr);
            }

            extraAttributes.Clear();
            extraAttributes = null;
        }
        #endregion
    }
}

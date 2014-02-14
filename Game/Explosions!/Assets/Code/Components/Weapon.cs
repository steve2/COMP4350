using System;
using UnityEngine;
using System.Collections;
using Assets.Code.Interfaces;

namespace Assets.Code.Components
{
    [RequireComponent(typeof(AttributeManager))]
    class Weapon : MonoBehaviour, IUsableItem
    {
        public bool Use()
        {
            //TODO: Implement
            throw new NotImplementedException();
        }

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}

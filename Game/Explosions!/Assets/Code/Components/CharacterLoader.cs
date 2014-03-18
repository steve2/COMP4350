using UnityEngine;
using System.Collections;
using Assets.Code.Model;

namespace Assets.Code.Components
{
    /// <summary>
    /// The component for Character
    /// Other components will use this component to speak with the character
    /// </summary>
    [RequireComponent(typeof(Inventory))]
    [RequireComponent(typeof(AttributeManager))]
    [RequireComponent(typeof(EquipmentManager))]
    public class CharacterLoader : GameComponent
    {
        public Inventory Inventory { get; private set; }
        public AttributeManager AttributeManager { get; private set; }
        public EquipmentManager EquipmentManager { get; private set; }

        //TODO: It would be nice to store what is already disabled and NOT enable it afterward
        private void EnableAll()
        {
            gameObject.SetActive(true);
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(true);
            }
        }

        private void Hide()
        {
            gameObject.SetActive(false); //Auto recurses down

            //Only activate a minimal subset
            //gameObject.SetActive(true);
            Inventory.enabled = true;
            AttributeManager.enabled = true;
            EquipmentManager.enabled = true;
        }

        public void Awake()
        {
            Inventory = GetComponent<Inventory>();
            AttributeManager = GetComponent<AttributeManager>();
            EquipmentManager = GetComponent<EquipmentManager>();
            Hide();
        }

        /// <summary>
        /// Call this when the scene is loaded and we want to put the character in it
        /// </summary>
        public void Show()
        {
            EnableAll();
        }
    }
}
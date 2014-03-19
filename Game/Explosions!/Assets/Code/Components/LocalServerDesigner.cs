using UnityEngine;
using System.Collections;
using Assets.Code.Controller;
using System.Collections.Generic;
using Assets.Code.Model;
using System;

namespace Assets.Code.Components
{
    public class LocalServerDesigner : MonoBehaviour
    {
        /// <summary>
        /// Inspector doesn't support key value pairs
        /// </summary>
        [Serializable]
        private class ItemQuantityPair
        {
            public Item item;
            public int quantity;
        }
        [Serializable]
        private class ItemSlotPair
        {
            public Item item;
            public Slot slot;
        }

        [SerializeField]
        List<ItemQuantityPair> inventory;
        [SerializeField]
        List<ItemSlotPair> equipment; //The index maps to the slot
        [SerializeField]
        Character character;

        // Called before any component calls Start
        void Awake()
        {
            LocalServer server = new LocalServer();
            server.SetInventory(ConvertInventory(inventory));
            server.SetEquipment(ConvertEquipment(equipment));
            //TODO: May need to instantiate the prefab?
            //Inject local server
            Game.Init(server);
            Game.Instance.SetCharacter(Character.CreateNew("Demo Character"));
            Debug.Log("Loading Character");
            Game.Instance.LoadCharacter();
	    }

        private IEnumerable<KeyValuePair<string, int>> ConvertInventory(List<ItemQuantityPair> inventory)
        {
            foreach (ItemQuantityPair iqp in inventory)
            {
                yield return new KeyValuePair<string, int>(iqp.item.Name, iqp.quantity);
            }
        }

        private IEnumerable<KeyValuePair<string, Slot>> ConvertEquipment(List<ItemSlotPair> equipment)
        {
            foreach (ItemSlotPair isp in equipment)
            {
                yield return new KeyValuePair<string, Slot>(isp.item.Name, isp.slot);
            }
        }

    }
}

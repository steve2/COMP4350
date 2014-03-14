using UnityEngine;
using System.Collections;
using Assets.Code.Controller;
using System.Collections.Generic;
using Assets.Code.Model;

namespace Assets.Code.Components
{
    public class LocalServerDesigner : MonoBehaviour
    {
        [SerializeField]
        List<Item> inventory;
        [SerializeField]
        List<Item> equipment; //The index maps to the slot

        // Called before any component calls Start
        void Awake()
        {
            LocalServer server = new LocalServer();
            server.SetInventory(inventory);
            server.SetEquipment(equipment);
            //TODO: May need to instantiate the prefab?
            //Inject local server
            Game.Init(server);
	    }
    }
}

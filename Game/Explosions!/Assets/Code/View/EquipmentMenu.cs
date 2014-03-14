using UnityEngine;
using System.Collections;
using Assets.Plugins;
using Assets.Code.Components;
using System.Collections.Generic;
using Assets.Code.Model;

[RequireComponent(typeof(EquipmentManager))]
[RequireComponent(typeof(Inventory))]
public class EquipmentMenu : MonoBehaviour {
    private bool showMenu;
    private EquipmentManager equipment;
    private Inventory inventory;

    public void Start()
    {
        showMenu = false;
        equipment = GetComponent<EquipmentManager>();
        inventory = GetComponent<Inventory>();
    }

    void OnGUI()
    {
        GUILayout.BeginArea(GUIPlus.LayoutRect(0.10f, 0.05f, GUIAlign.BottomRight));
        if (GUILayout.Button("Equipment", GUILayout.Height(40)) || Input.GetAxis("EquipmentMenu") > 0)
        {
            showMenu = !showMenu;
        }
        GUILayout.EndArea();
        if (showMenu)
        {
            GUI.Window(0, GUIPlus.LayoutRect(0.3f, 0.75f, GUIAlign.Center), ShowEquipment, "Equipment");
        }
    }

    private void ShowEquipment(int id)
    {
        const int HEIGHT = 30;

        GUILayout.Label("Equipped");
        foreach (var equip in equipment)
        {
            Item item = equip.Value;
            GUILayout.BeginHorizontal();
            GUILayout.Label("\t" + item.ToString()); 
            if (GUILayout.Button("Dequip", GUILayout.Height(HEIGHT)))
            {
                equipment.Dequip(equip.Key);
                break;
            }
            GUILayout.EndHorizontal();
        }
        GUILayout.Label("Inventory");
        foreach (Item item in inventory)
        {
            int quantity = inventory.GetQuantity(item);
            if (quantity > 0)
            {
                GUILayout.BeginHorizontal();
                GUILayout.Label("\t" + item.Name + "\t(x" + quantity +")");
                //TODO: Drop down list
                if (GUILayout.Button("Equip", GUILayout.Height(HEIGHT)))
                {
                    equipment.Equip(item, Slot.RightHand);
                    break;
                }
                GUILayout.EndHorizontal();
            }
        }
    }
}

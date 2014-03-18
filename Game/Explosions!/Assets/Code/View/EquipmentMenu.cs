using UnityEngine;
using System.Collections;
using Assets.Plugins;
using Assets.Code.Components;
using System.Collections.Generic;
using Assets.Code.Model;

[RequireComponent(typeof(EquipmentManager))]
[RequireComponent(typeof(Inventory))]
public class EquipmentMenu : MonoBehaviour 
{
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

    private static readonly GUILayoutOption[] EQUIP = { GUILayout.Height(30), GUILayout.Width(60) };
    private void ShowEquipment(int id)
    {
        GUILayout.Label("Equipped");
        foreach (var equip in equipment)
        {
            Item item = equip.Value;
            GUILayout.BeginHorizontal();
            GUILayout.Label("\t" + item.ToString() + "\t(" + equip.Key + ")" ); 
            if (GUILayout.Button("Dequip", EQUIP))
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
				ShowSlots (item);
                if (GUILayout.Button("Equip", EQUIP))
                {
                    equipment.Equip(item, GetSlot (item)); //TODO: Assign slot properly...
                    break;
                }
                GUILayout.EndHorizontal();
            }
        }
    }

	private Slot GetSlot(Item item)
	{
		//TODO: What if this fails?
		return equipment.GetSlots(item.Type)[slotIdx[item.Name]];
	}

    private static readonly GUILayoutOption[] SLOT = { GUILayout.Height(30), GUILayout.Width(180) };
    private Dictionary<string, int> slotIdx = new Dictionary<string, int>();
    private void ShowSlots(Item item)
    {
        //TODO: Try to reduce the number of array copies
        IEnumerable<Slot> slots = equipment.GetSlots(item.Type);
        List<string> slotStrs = new List<string>();
        foreach (Slot s in slots)
        {
            slotStrs.Add(s.ToString());
        }

        if(slotIdx.ContainsKey(item.Name))
        {
            slotIdx[item.Name] = GUILayout.Toolbar(slotIdx[item.Name], slotStrs.ToArray(), SLOT);
        }
        else
        {
            slotIdx.Add(item.Name, GUILayout.Toolbar(0, slotStrs.ToArray(), SLOT));
        }
    }
}

using UnityEngine;
using System.Collections;
using Assets.Plugins;

public class EquipmentMenu : MonoBehaviour {
    private bool showMenu;

    public void Start()
    {
        showMenu = false;
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
            GUI.Window(0, GUIPlus.LayoutRect(0.5f, 0.75f, GUIAlign.Center), ShowEquipment, "Equipment");
        }
    }

    private void ShowEquipment(int id)
    {
        GUILayout.Label("Equipped");
        GUILayout.Label("Inventory");
    }
}

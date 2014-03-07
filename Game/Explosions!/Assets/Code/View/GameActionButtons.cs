using UnityEngine;
using System.Collections;
using Assets.Plugins;

[RequireComponent(typeof(GameActionController))]
public class GameActionButtons : MonoBehaviour 
{
    private GameActionController controls;

    public void Start()
    {
        controls = GetComponent<GameActionController>();
    }

    // Called when the GUI is rendered
    void OnGUI()
    {
        GUILayout.BeginArea(GUIPlus.LayoutRect(0.5f, 0.05f, GUIAlign.Bottom));
        GUILayout.BeginHorizontal("Actions");
        foreach (string action in controls)
        {
            if (GUILayout.RepeatButton(action, GUILayout.Width(80), GUILayout.Height(40)))
            {
                controls.Perform(action);
            }
        }
        GUILayout.EndHorizontal();
        GUILayout.EndArea();
    }
}


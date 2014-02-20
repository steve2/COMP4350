using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

/// <summary>
/// An input adapter used to inject custom values into the input
/// </summary>
public static class InputAdapter
{
    private static Dictionary<string, float> values = new Dictionary<string,float>();

    public static void SetAxisRaw(string axisName, float value)
    {
        if (values.ContainsKey(axisName))
        {
            values[axisName] = Mathf.Clamp(value, -1f, 1f);
        }
		else
		{
			values.Add (axisName, value);
		}
    }

    public static float GetAxisRaw(string axisName)
    {
        float injectedValue;
        if (values.TryGetValue(axisName, out injectedValue) && injectedValue != 0)
        {
        	return injectedValue;
        }

		return Input.GetAxisRaw(axisName);
    }

	public static void ResetInputAxis()
	{
		InputAdapter.SetAxisRaw ("Vertical", 0);
		InputAdapter.SetAxisRaw ("Horizontal", 0);
	}

	public static bool GetButtonDown(string buttonName)
	{
		return false;
	}

	public static bool GetKey(string name)
	{
		return false;
	}
}

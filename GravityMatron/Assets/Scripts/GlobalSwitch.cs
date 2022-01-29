using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GlobalSwitch 
{
    public static UnityAction<bool> SwitchModes;
    public static bool currentMode;

    public static void SwitchModeTo(bool mode)
    {
        currentMode = mode;
        SwitchModes.Invoke(mode);
    }
}

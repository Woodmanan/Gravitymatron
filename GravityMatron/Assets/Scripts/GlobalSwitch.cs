using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GlobalSwitch 
{
    public static UnityAction<SwitchMode> SwitchModes;
    public static SwitchMode currentMode = SwitchMode.TopDown;

    static GlobalSwitch()
    {
        SwitchModes += OnSwitchMode;
    }
    
    public static void SwitchModeTo(SwitchMode mode)
    {
        currentMode = mode;
        SwitchModes?.Invoke(mode);
    }

    private static void OnSwitchMode(SwitchMode to)
    {
        Debug.Log($"Mode switched to {to}");
    }
}

public enum SwitchMode
{
    TopDown,
    SideScroller
}
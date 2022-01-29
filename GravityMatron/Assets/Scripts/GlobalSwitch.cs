using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GlobalSwitch 
{
    public static UnityAction<SwitchMode> SwitchModes;
    public static SwitchMode currentMode;

    public static void SwitchModeTo(SwitchMode mode)
    {
        currentMode = mode;
        SwitchModes.Invoke(mode);
    }
}

public enum SwitchMode
{
    TopDown,
    SideScroller
}
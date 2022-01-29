using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalSTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GlobalSwitch.SwitchModes += A;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick()
    {
        if (GlobalSwitch.currentMode == SwitchMode.TopDown)
        {
            GlobalSwitch.SwitchModeTo(SwitchMode.SideScroller);
        }
        else
        {
            GlobalSwitch.SwitchModeTo(SwitchMode.TopDown);
        }

    }

    public void A(SwitchMode s)
    {
        Debug.Log(s);
    }
}

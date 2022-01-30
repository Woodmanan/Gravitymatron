using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchSite : MonoBehaviour
{
    public string url;
    
    public void OpenSite()
    {
        Application.OpenURL(url);
    }
}

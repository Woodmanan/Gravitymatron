using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public GameObject selectOnReturn;
    public GameObject selectOnCredits;

    public void LoadCredits()
    {
        GetComponent<Animator>().SetTrigger("LoadCredits");
        EventSystem.current.SetSelectedGameObject(selectOnCredits);
    }

    public void LeaveCredits()
    {
        GetComponent<Animator>().SetTrigger("ExitCredits");
        EventSystem.current.SetSelectedGameObject(selectOnReturn);
    }

    public void Quit()
    {
        Application.Quit();
    }
}

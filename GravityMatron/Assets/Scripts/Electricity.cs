using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Electricity : MonoBehaviour
{
    public SwitchMode activeMode;
    public float length;
    public float width;

    public Color activeColor;
    public Color inactiveColor;

    // Start is called before the first frame update
    void Start()
    {
        GlobalSwitch.SwitchModes += Switch;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Switch(SwitchMode mode)
    {
        if ((GlobalSwitch.currentMode & activeMode) > 0)
        {
            GetComponent<SpriteRenderer>().color = activeColor;
        }
        else
        {
            GetComponent<SpriteRenderer>().color = inactiveColor;
        }
    }

    private void OnValidate()
    {
        GetComponent<SpriteRenderer>().size = new Vector2(length, 1);
        GetComponent<BoxCollider2D>().size = new Vector2(length, width);
        GetComponent<SpriteRenderer>().color = ((activeMode & GlobalSwitch.currentMode) > 0) ? activeColor : inactiveColor;
        transform.GetChild(0).localPosition = new Vector3(length / 2 + 0.5f, 0, 0);
        transform.GetChild(1).localPosition = new Vector3(-length / 2 - 0.5f, 0, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((GlobalSwitch.currentMode & activeMode) > 0)
        {
            if (collision.tag.Equals("Player"))
            {
                collision.GetComponent<PlayerController>().Kill();
            }
        }
    }
}

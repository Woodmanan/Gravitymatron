using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector2 direction;
    private float speed;
    private SwitchMode activeMode;

    private Rigidbody2D rb2d;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        GlobalSwitch.SwitchModes += OnStateChanged;
    }

    public void Initialize(Vector2 direction, float speed, SwitchMode activeMode)
    {
        this.direction = direction;
        this.speed = speed;
        this.activeMode = activeMode;
    }

    public void OnStateChanged(SwitchMode newMode)
    {
        if (newMode == activeMode)
        {
            rb2d.velocity = direction * speed;
        }
        else
        {
            rb2d.velocity = Vector2.zero;
        }
    }

}

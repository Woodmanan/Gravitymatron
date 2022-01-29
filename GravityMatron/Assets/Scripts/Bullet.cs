using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector2 direction;
    private float speed;
    private SwitchMode activeMode;

    private bool touching;

    private Rigidbody2D rb2d;
    
    // Start is called before the first frame update
    void Awake()
    {
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        GlobalSwitch.SwitchModes += OnStateChanged;
    }

    public void Initialize(Vector2 direction, float speed, SwitchMode activeMode)
    {
        this.direction = direction;
        this.speed = speed;
        this.activeMode = activeMode;
        rb2d.velocity = direction * speed;
        touching = false;
    }

    public void OnStateChanged(SwitchMode newMode)
    {
        if (newMode == activeMode)
        {
            if (touching)
            {
                KillPlayer();
            }
            rb2d.velocity = direction * speed;
        }
        else
        {
            rb2d.velocity = Vector2.zero;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (GlobalSwitch.currentMode == activeMode)
        {
            KillPlayer();
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        touching = false;
    }

    private void KillPlayer()
    {

    }

}

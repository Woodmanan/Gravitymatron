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
        rb2d.bodyType = RigidbodyType2D.Dynamic;
        rb2d.velocity = direction * speed;
    }

    public void OnStateChanged(SwitchMode newMode)
    {
        if (newMode == activeMode)
        {
            gameObject.layer = 8;
            rb2d.bodyType = RigidbodyType2D.Dynamic;
            rb2d.velocity = direction * speed;
        }
        else
        {
            rb2d.bodyType = RigidbodyType2D.Static;
            gameObject.layer = 3;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (GlobalSwitch.currentMode == activeMode)
        {
            if (collision.gameObject.tag == "Player")
            {
                collision.gameObject.GetComponent<PlayerController>().Kill();
            }

            if (gameObject.layer == 8)
            {
                BulletPool.Instance.StoreBullet(gameObject);
            }
        }
    }

}

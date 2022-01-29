using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector2 direction;
    private float speed;
    public SwitchMode activeMode;

    private Rigidbody2D rb2d;

    public Color activeColor;
    public Color inactiveColor;
    public float activeGlowIntensity;
    public float inactiveGlowIntensity;

    // Start is called before the first frame update
    void Awake()
    {
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        GlobalSwitch.SwitchModes += OnStateChanged;
        OnStateChanged(GlobalSwitch.currentMode);
    }

    public void Initialize(Vector2 direction, float speed, SwitchMode activeMode)
    {
        this.direction = direction;
        this.speed = speed;
        this.activeMode = activeMode;
        rb2d.bodyType = RigidbodyType2D.Dynamic;
        rb2d.velocity = direction * speed;
        OnStateChanged(GlobalSwitch.currentMode);
    }

    public void OnStateChanged(SwitchMode newMode)
    {
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        if ((newMode & activeMode) > 0)
        {
            gameObject.layer = 8;
            rb2d.bodyType = RigidbodyType2D.Dynamic;
            rb2d.velocity = direction * speed;
            renderer.material.color = activeColor;
            float factor = Mathf.Pow(2f, activeGlowIntensity);
            Color bright = new Color(activeColor.r * factor, activeColor.g * factor, activeColor.b * factor);
            renderer.material.SetColor("_EmissionColor", bright);
        }
        else
        {
            rb2d.bodyType = RigidbodyType2D.Static;
            gameObject.layer = 10; //Semisolid
            renderer.material.color = inactiveColor;
            float factor = Mathf.Pow(2f, inactiveGlowIntensity);
            Color bright = new Color(inactiveColor.r * factor, inactiveColor.g * factor, inactiveColor.b * factor);
            renderer.material.SetColor("_EmissionColor", bright);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if ((GlobalSwitch.currentMode & activeMode) > 0)
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

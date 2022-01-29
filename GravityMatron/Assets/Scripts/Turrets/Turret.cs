using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Turret : MonoBehaviour
{
    public float bulletSpeed;
    public SwitchMode activeMode;
    public bool followPlayer;
    public float rotationSpeed;
    public float attackInterval;

    private GameObject target;
    public PlayerController playerController;
    private Transform firePosition;
    private float timePassed;

    private float angle;

    // Start is called before the first frame update
    void Start()
    {
        firePosition = transform.GetChild(0);
        target = GameObject.FindGameObjectWithTag("Player");
        playerController = target.GetComponent<PlayerController>();
        timePassed = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (GlobalSwitch.currentMode == activeMode)
        {
            timePassed += Time.deltaTime;
            Rotate();
        }

        if (timePassed > attackInterval)
        {
            timePassed = 0;
            float distance = Mathf.Abs(target.transform.position.y - transform.position.y);
            if (!playerController.canToggleModes && distance < 14)
            {
                Attack();
            }
        }
    }

    public void Rotate()
    {
        if (followPlayer)
        {
            Vector2 dir = target.transform.position - transform.position;
            angle = Mathf.Atan2(dir.y, dir.x);

            float convertedAngle = angle * Mathf.Rad2Deg - 90;
            gameObject.transform.rotation = Quaternion.Euler(0, 0, convertedAngle);
        }
        else
        {
            angle -= rotationSpeed;
            if (angle > 2 * 3.1416)
            {
                angle = 0;
            }
            float convertedAngle = angle * Mathf.Rad2Deg - 90;
            gameObject.transform.rotation = Quaternion.Euler(0, 0, convertedAngle);
        }
    }

    public void FireBullet(float addedAngle)
    {
        GameObject newBullet = BulletPool.Instance.GetBullet();
        newBullet.transform.position = firePosition.position;

        float outAngle = angle + addedAngle;
        Vector2 newDir = new Vector2(Mathf.Cos(outAngle), Mathf.Sin(outAngle));
        newBullet.GetComponent<Bullet>().Initialize(newDir, bulletSpeed, activeMode);
    }

    public virtual void Attack()
    {
        
    }
}
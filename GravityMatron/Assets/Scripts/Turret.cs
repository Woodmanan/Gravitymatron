using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public GameObject bullet;
    public Transform firePosition;
    public float bulletSpeed;

    public GameObject target;

    private float angle;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            FireBullet();
        }

        Vector2 dir = target.transform.position - transform.position;
        angle = Mathf.Atan2(dir.y, dir.x);

        float convertedAngle = angle * Mathf.Rad2Deg - 90;
        gameObject.transform.rotation = Quaternion.Euler(0, 0, convertedAngle);
    }

    public void FireBullet()
    {
        GameObject newBullet = Instantiate(bullet);
        newBullet.transform.position = firePosition.position;

        Vector2 newDir = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
        newBullet.GetComponent<Bullet>().Initialize(newDir, bulletSpeed, SwitchMode.SideScroller);
    }
}

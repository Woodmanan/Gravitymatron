using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sector : Turret
{
    public float angle;
    public int num;

    public override void Attack()
    {
        for (int i = 0; i < num; i++)
        {
            float bulletAngle = - angle / 2;
            bulletAngle += angle * i / num;
            FireBullet(bulletAngle);
        }
    }
}

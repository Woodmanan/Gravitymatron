using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sector : Turret
{
    public float sectorAngle;
    public int num;

    public override void Attack()
    {
        base.Attack();

        for (int i = 0; i < num; i++)
        {
            float bulletAngle = -sectorAngle / 2;
            bulletAngle += sectorAngle * i / num;
            FireBullet(bulletAngle);
        }
    }
}

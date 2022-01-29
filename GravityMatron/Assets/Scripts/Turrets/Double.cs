using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Double : Turret
{
    public override void Attack()
    {
        base.Attack();

        FireBullet(0);
        FireBullet(Mathf.PI);
    }
}

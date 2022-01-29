using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basic: Turret
{
    public override void Attack()
    {
        base.Attack();

        FireBullet(0);
    }
}

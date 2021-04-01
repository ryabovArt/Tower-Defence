using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : Health
{
    public override void GetDamage()
    {
        health -= getDamage;
        Debug.Log(health);
        if (health <= 0) Destroy(gameObject);
    }
}

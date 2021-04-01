using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerDamage : Health
{
    public Collider collider;

    private void Update()
    {
        if (collider.CompareTag("Bullet")) GetDamage();
    }

    public override void GetDamage()
    {
        health -= 10;
        //Debug.Log(health);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barricade : MonoBehaviour
{
    private BarricadeHealthBar healthBar;

    private float health;
    private float maxHealth;
     public int Health
    {
        set
        {
            health = value;
        }
    }
    private void Start()
    {
        health = GetComponent<BuildingProperties>().item.BuildingHealth;
        healthBar = transform.GetComponentInChildren<BarricadeHealthBar>();
        maxHealth = health;
    }

    public void RecieveDamage(float damage)
    {
        health -= damage;
        healthBar.UpdateHealthBar(health,maxHealth);
        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}

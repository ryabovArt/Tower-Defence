 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : Singleton<Goal>
{
    private float health;
    [SerializeField] private float maxHealth;
    [SerializeField] private GoalHealthBar healthBar;
    private ChangeScene sM;

    private void Start()
    {
        health = maxHealth;
        sM = FindObjectOfType<ChangeScene>();
        healthBar.UpdateHealthBar(health, maxHealth);
    }

    //private 
    // Start is called before the first frame update
    public void RecieveDamage(float damage)
    {
        health -= damage;
        healthBar.UpdateHealthBar(health, maxHealth);
        if (health <= 0)
        {
            Debug.Log("Loose");
            sM.Lose();
        }
    }

    public void Heal(float heal)
    {
        if (health + heal <= maxHealth)
        {
            health += heal;
        }
        else
        {
            health = maxHealth;
        }
        healthBar.UpdateHealthBar(health, maxHealth);
    }
}

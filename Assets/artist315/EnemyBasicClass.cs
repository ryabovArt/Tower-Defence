using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class EnemyBasicClass : MonoBehaviour
{
    // ссылки на UI Моба
    //[SerializeField] internal Image healthBar;
    [SerializeField] internal HealthBar healthBar;
    [SerializeField] internal Image[] EffectBars = new Image[2];
    public AssetEnemy enemyPref;
    
    internal float damage;
    internal float attackRate;
    internal int reward;
    internal float speed;
    internal float health;
    internal float MaxHealth;
    internal float armour;
    internal DamageTypes[] damageEffects = new DamageTypes[2] { DamageTypes.Phisical, DamageTypes.Phisical };

    private void OnEnable()
    {
        SetProperties(enemyPref);
        EffectsAnimation();
        //Debug.Log(health);
    }
    public void RecieveDamage(float damage)
    {
        if (damage >= armour)
        {

            health -= damage - armour;
            //healthBar.UpdateHealthBar(health, MaxHealth);// = health / MaxHealth;
        }
        if (healthBar.UpdateHealthBar(health, MaxHealth) < 0)
        {
            Destroy(this.gameObject);
        }
    }

    public void RecieveDamage(float damage, DamageTypes damageEffects)
    {
        if (damage >= armour)
        {
            health -= damage - armour;
            
            //healthBar.fillAmount = health / MaxHealth;
        }
        if (healthBar.UpdateHealthBar(health, MaxHealth) <= 0)
        {
            Destroy(this.gameObject);
        }
        if (damageEffects != DamageTypes.Phisical)
        {
            if (this.damageEffects[0] == DamageTypes.Phisical)
            {
                this.damageEffects[0] = damageEffects;
                EffectsAnimation();
            }
            else if(damageEffects != this.damageEffects[0])
            {
                this.damageEffects[1] = damageEffects;
                ApplyEffectsCombo();
                for (int i = 0; i < this.damageEffects.Length; i++)
                {
                    this.damageEffects[i] = DamageTypes.Phisical;
                }
                EffectsAnimation();
            }
        }
    }

    private void EffectsAnimation()
    {
        for (int i = 0; i < damageEffects.Length; i++)
        {
            if (damageEffects[i] == DamageTypes.Phisical)
            {
                EffectBars[i].enabled = false;
            }
            else
            {
                EffectBars[i].enabled = true;
                EffectBars[i].color = EffectColour(damageEffects[i]);
            }
        }

    }

    private Color EffectColour(DamageTypes damageTypes)
    {
        Color color = Color.black;
        switch (damageTypes)
        {            
            case DamageTypes.Phisical:
                color = Color.clear;
                break;
            case DamageTypes.Fire:
                color = Color.red;
                break;
            case DamageTypes.Water:
                color = Color.blue;
                break;
            case DamageTypes.Electricity:
                color = new Color(200,225,0);
                break;
            case DamageTypes.Ice:
                color = new Color(0, 225, 202);
                break;
            default:
                break;
        }
        return color;
    }

    private void ApplyEffectsCombo()
    {

    }

    private void SetProperties(AssetEnemy enemySO)
    {
        damage = enemySO.Damage;
        speed = enemySO.Speed;
        GetComponent<NavMeshAgent>().speed = speed;
        health = enemySO.EnemyHealth;
        armour = enemySO.Armour;
        reward = enemySO.Reward;
        MaxHealth = health;
        attackRate = enemySO.AttackRate;

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Target"))
        {
            other.GetComponent<Goal>().RecieveDamage(damage);
            Destroy(gameObject);
        }
        else if (other.gameObject.CompareTag("Obstacle"))
        {
            StartCoroutine(SendDamage(other));//other.GetComponent<Barricade>()
            //other.GetComponent<Barricade>().RecieveDamage(damage);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            StopAllCoroutines();
        }
    }

    private void OnDestroy()
    {
        ManaScript.Instance.RewardInc(reward);
    }

    IEnumerator SendDamage(Collider barricade)
    {
        while (true)
        {
            yield return new WaitForSeconds(attackRate);
            //Debug.Log(barricade.gameObject);
            barricade.GetComponent<Barricade>().RecieveDamage(damage);
        }
        

    }
}

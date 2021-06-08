using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoalHealthBar : MonoBehaviour
{
    [SerializeField] internal Image healthBar;
    [SerializeField] private Text text;

    public float UpdateHealthBar(float health, float MaxHealth)
    {        
        healthBar.fillAmount = health / MaxHealth;
        text.text = health.ToString();
        return healthBar.fillAmount;
    }

}

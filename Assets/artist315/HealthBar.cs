using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Transform cam;
    [SerializeField] internal Image healthBar;
    private void OnEnable()
    {
        cam = FindObjectOfType<Camera>().GetComponent<Transform>();
    }

    private void LateUpdate()
    {
        //transform.LookAt( cam.position);
    }

    public float UpdateHealthBar(float health, float MaxHealth)
    {
        healthBar.fillAmount = health / MaxHealth;
        if (healthBar.fillAmount > 0.60)
        {
            healthBar.color = Color.green;
        }
        else if (healthBar.fillAmount < 0.3)
        {
            healthBar.color = Color.red;
        }
        else
        {
            healthBar.color = Color.yellow;
        }
       
        return healthBar.fillAmount;
    }
}

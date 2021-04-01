using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : EntityProperties
{
    private NavMeshAgent meshAgent;
    private float speed;
    private GameObject damageTarget;


    private void Start()
    {
        meshAgent = GetComponent<NavMeshAgent>();
        meshAgent.speed = speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Obstacle"))
        {
            damageTarget = other.gameObject;
            StartCoroutine(SetDamage());
        }
    }

    IEnumerator SetDamage()
    {
        if(damageTarget != null)
        {
            if (damageTarget.GetComponent<BuildingProperties>().health > 1)
            {
                damageTarget.GetComponent<BuildingProperties>().health -= damage;
                Debug.Log(damageTarget.GetComponent<BuildingProperties>().health);
                yield return new WaitForSeconds(1f);
                StartCoroutine(SetDamage());
            }
            else if (damageTarget.GetComponent<BuildingProperties>().health <= 1)
            {
                StopCoroutine(SetDamage());
                damageTarget = null;
                //Destroy(damageTarget);
            }
        }

        //if (damageTarget.GetComponent<BuildingProperties>().health <= 0)
        //    Destroy(damageTarget);
        //if(damageTarget.GetComponent<BuildingProperties>().health > 0)

    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class EnemySetDamage : MonoBehaviour
{
    private GameObject damageTarget;
    [SerializeField] private int damage;
    private NavMeshAgent meshAgent;
    public UnityEvent DestroyObstacleEvent;

    private void Start()
    {
        meshAgent = GetComponent<NavMeshAgent>();
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
        if (damageTarget != null && GetComponent<EnemiesMover>().path.status == NavMeshPathStatus.PathPartial)
        {
            if (damageTarget.GetComponent<BuildingProperties>().BuildingHealthState > 1)
            {
                damageTarget.GetComponent<BuildingProperties>().BuildingHealthState -= damage;
                Debug.Log(damageTarget.GetComponent<BuildingProperties>().BuildingHealthState);
                yield return new WaitForSeconds(3f);
                StartCoroutine(SetDamage());
            }
            else if (damageTarget.GetComponent<BuildingProperties>().BuildingHealthState < 1)
            {
                StopCoroutine(SetDamage());
                damageTarget = null;
                DestroyObstacleEvent.Invoke();
                //Destroy(damageTarget);
            }
        }
        else if(GetComponent<EnemiesMover>().path.status == NavMeshPathStatus.PathComplete) 
            StopCoroutine(SetDamage()); 
    }
    //IEnumerator SetDamage()
    //{
    //    if (damageTarget != null)
    //    {
    //        if (damageTarget.GetComponent<BuildingProperties>().health > 1)
    //        {
    //            damageTarget.GetComponent<BuildingProperties>().health -= damage;
    //            Debug.Log(damageTarget.GetComponent<BuildingProperties>().health);
    //            yield return new WaitForSeconds(1f);
    //            StartCoroutine(SetDamage());
    //        }
    //        else if (damageTarget.GetComponent<BuildingProperties>().health <= 1)
    //        {
    //            StopCoroutine(SetDamage());
    //            damageTarget = null;
    //            //Destroy(damageTarget);
    //        }
    //    }

    //    //if (damageTarget.GetComponent<BuildingProperties>().health <= 0)
    //    //    Destroy(damageTarget);
    //    //if(damageTarget.GetComponent<BuildingProperties>().health > 0)

    //}
}

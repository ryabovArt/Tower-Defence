﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemiesMover : MonoBehaviour
{
    private Transform target; // 
    private Vector3 originalTarget;
    private NavMeshAgent meshAgent;
    private NavMeshPath path; // путь

    private GameObject currentObstacle; // текущее препятствие

    internal bool isDestroy = false; // уничтожено ли препятствие

    //Временные переменные
    private int hit;
    private Text text;

    void Start()
    {
        text = WaveSpawn.instance.text;
        target = WaveSpawn.instance.target;
        meshAgent = GetComponent<NavMeshAgent>();
        meshAgent.SetDestination(target.position);
        EnemiesOnScene.instance.enemiesOnScene.Add(this.gameObject);
        originalTarget = target.position;
        StartCoroutine(UpdateDestination());
        path = new NavMeshPath();
    }

    

    /// <summary>
    /// Пересчет пути
    /// </summary>
    /// <returns></returns>
    IEnumerator UpdateDestination()
    {
        if (!isDestroy)
            meshAgent.SetDestination(originalTarget);

        yield return new WaitForSeconds(1f);

        meshAgent.CalculatePath(originalTarget, path);
        //Debug.Log(path.status);

        if (path.status == NavMeshPathStatus.PathPartial/*path.status != NavMeshPathStatus.PathComplete*/)
        {
            ChangeTargetPosition(PlacingBuilding.instance.buildObs);
            StartCoroutine(DestroyObstacle());
        }
        if (path.status == NavMeshPathStatus.PathComplete)
        {
            isDestroy = !isDestroy;
            meshAgent.speed = 0.5f;
        }

        StartCoroutine(UpdateDestination());
    }

    /// <summary>
    /// Ищем ближайшие препятствия
    /// </summary>
    /// <param name="objects"> препятствия </param>
    /// <returns> таргет </returns>
    private Transform ChangeTargetPosition(List<GameObject> objects)
    {
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject obj in objects)
        {
            Vector3 diff = obj.transform.position - position;
            float currentDis = diff.sqrMagnitude;
            if (currentDis < distance)
            {
                currentObstacle = obj;
                distance = currentDis;
                isDestroy = true;
                meshAgent.SetDestination(currentObstacle.transform.position); 
            }
        }
        return target;
    }

    /// <summary>
    /// Уничтожение препятствия
    /// </summary>
    /// <returns></returns>
    IEnumerator DestroyObstacle()
    {
        if(currentObstacle != null)
        {
            if ((currentObstacle.transform.position - meshAgent.transform.position).magnitude < 1.5f && currentObstacle != null)
            {
                meshAgent.speed = 0;
                if (/*hit >= 4*/currentObstacle.GetComponent<BuildingProperties>().health <= 1)
                {
                    foreach (var obs in PlacingBuilding.instance.Way)
                    {
                        if (Vector3.Distance(currentObstacle.transform.position, obs.transform.position) < 0.3f
                               && obs.tag == "FullPlace")
                        {
                            obs.tag = "Way";
                        }
                    }

                    PlacingBuilding.instance.buildObs.Remove(currentObstacle);
                    Destroy(currentObstacle.gameObject);
                    target.position = originalTarget;
                    text.text = string.Empty;
                    hit = 0;
                    isDestroy = false;
                    meshAgent.speed = 0.5f;
                    yield break;
                }

                text.text = $"Attack! {hit}";
                hit++;
            }
            if (/*(closest.transform.position - meshAgent.transform.position).magnitude > 1.5f 
                && */ path.status == NavMeshPathStatus.PathPartial)
                meshAgent.speed = 0.5f;
        }

        yield return new WaitForSeconds(1f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Target"))
        {
            Destroy(gameObject);
            //Debug.Log("222");
        }
        //else if(other.CompareTag("Bullet"))
        //{
        //    GetComponent<EnemyDamage>().GetDamage();
        //}
    }
}
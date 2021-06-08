using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemiesMover : MonoBehaviour
{
    private Transform target;
    private Vector3 originalTarget;
    private NavMeshAgent meshAgent;
    internal NavMeshPath path; // путь

    private GameObject currentObstacle; // текущее препятствие

    internal bool isDestroy = false; // уничтожено ли препятствие

    //Временные переменные
    private int hit;
    private Text text;
    private float sp;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Target").transform;
        //text = SpawnWaves.instance.text;
        //target = SpawnWaves.instance.target;
        meshAgent = GetComponent<NavMeshAgent>();
        meshAgent.SetDestination(target.position);
        EnemiesOnScene.instance.enemiesOnScene.Add(this.gameObject);
        originalTarget = target.position;
        StartCoroutine(UpdateDestination());
        path = new NavMeshPath();

        sp = meshAgent.speed;
    }

    /// <summary>
    /// Пересчет пути
    /// </summary>
    /// <returns></returns>
    public IEnumerator UpdateDestination()
    {
        if (!isDestroy)
            meshAgent.SetDestination(originalTarget);

        yield return new WaitForSeconds(1f);

        meshAgent.CalculatePath(originalTarget, path);
        //Debug.Log(path.status);

        if (path.status == NavMeshPathStatus.PathPartial/*path.status != NavMeshPathStatus.PathComplete*/)
        {
            ChangeTargetPosition(PlacingBuilding.instance.buildObs);
            //StartCoroutine(DestroyObstacle());
            //DestroyObstacle();

        }
        if (path.status == NavMeshPathStatus.PathComplete)
        {
            isDestroy = !isDestroy;
            meshAgent.speed = 0.5f;
        }

        StartCoroutine(UpdateDestination());
        meshAgent.speed = sp;
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
        GameObject currentObstacle = new GameObject();
        foreach (GameObject obj in objects)
        {
            float currentDis = Vector3.Distance(obj.transform.position, position);
            if (currentDis < distance)
            {
                currentObstacle = obj;
                distance = currentDis;
                isDestroy = true;
            }
        }
        if (currentObstacle != null)
        {
            meshAgent.SetDestination(currentObstacle.transform.position);
        }
        
        return target;
    }

    /// <summary>
    /// Уничтожение препятствия
    /// </summary>
    /// <returns></returns>
    public void DestroyObstacle()
    {
        if(currentObstacle != null)
        {
            if ((currentObstacle.transform.position - meshAgent.transform.position).magnitude < 1.5f /*&& currentObstacle != null*/)
            {
                //meshAgent.speed = 0;
                if (currentObstacle.GetComponent<BuildingProperties>().BuildingHealthState <= 1)
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
                    //meshAgent.speed = 0.5f;
                    //yield break;
                }

                //text.text = $"Attack! {hit}";
                hit++;
            }
        }
    }


}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class CharacterMover : MonoBehaviour
{
    [SerializeField] private Transform target;
    private Vector3 originalTarget;
    private NavMeshAgent meshAgent;
    private NavMeshPath path;

    private GameObject closest;

    private bool isDestroy = false;

    //Временные переменные
    private int hit;
    public Text text;
    
    void Start()
    {
        meshAgent = GetComponent<NavMeshAgent>();
        meshAgent.SetDestination(target.position);
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
        meshAgent.SetDestination(target.position);

        if (Vector3.Distance(transform.position, originalTarget) < 0.6f) Destroy(this.gameObject);

        yield return new WaitForSeconds(1f);

        meshAgent.CalculatePath(target.position, path);
        Debug.Log(path.status);

        if (path.status != NavMeshPathStatus.PathComplete && !isDestroy)
        {
            ChangeTargetPosition(BuildingsGrid.instance.obs);
            StartCoroutine(DestroyObstacle());
            isDestroy = false;
        }
        else
        {
            if (isDestroy)
            {
                target.position = originalTarget;
                isDestroy = false;
            }
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
                closest = obj;
                distance = currentDis;
                target.position = closest.transform.position;
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
        if ((target.position - meshAgent.transform.position).magnitude < 1.5f)
        {
            if (hit == 4 || target.position == originalTarget) yield break;

            text.text = $"Attack! {hit}";
            hit++;
            isDestroy = true;
        }

        yield return new WaitForSeconds(1f);

        if (hit == 3)
        {
            if (target.position == originalTarget)
            {
                yield break;
            }
            else
            {
                BuildingsGrid.instance.obs.Remove(closest);
                Destroy(closest.gameObject);
                target.position = originalTarget;
                text.text = string.Empty;
                hit = 0;
                isDestroy = true;
            }
        }
    }
}

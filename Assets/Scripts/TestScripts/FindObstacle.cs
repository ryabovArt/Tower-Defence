using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FindObstacle : MonoBehaviour
{
    public static FindObstacle instance = null;

    private Transform target;
    private Vector3 originalTarget;
    public NavMeshAgent meshAgent;
    private NavMeshPath path;
    internal GameObject closest;
    internal bool temp = false;

    //public List<GameObject> go = new List<GameObject>();

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        target = WaveSpawn.instance.target;
        meshAgent = GetComponent<NavMeshAgent>();
        meshAgent.SetDestination(target.position);
        originalTarget = target.position;
        path = new NavMeshPath();
        StartCoroutine(UpdateDestination());
    }

    IEnumerator UpdateDestination()
    {
        meshAgent.SetDestination(originalTarget);
        yield return new WaitForSeconds(0.5f);
        meshAgent.CalculatePath(originalTarget, path);

        if (path.status != NavMeshPathStatus.PathComplete)
        {
            ChangeTargetPosition(BuildingsGrid.instance.obs /*, temp*/);
            temp = false;
            Debug.Log("path");
        }
        else if(path.status == NavMeshPathStatus.PathComplete)
        {
            target.position = originalTarget;
            temp = true;
            Debug.Log("NOTpath");
        }
        StartCoroutine(UpdateDestination());
    }


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

        Debug.Log("12221");
        return target;
    }
}

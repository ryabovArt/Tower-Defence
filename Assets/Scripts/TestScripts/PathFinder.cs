using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    //private GameObject closest;

    //public GameObject FindClosestObstacle()
    //{
    //    float distance = Mathf.Infinity;
    //    Vector3 position = transform.position;
    //    foreach (GameObject obj in BuildingsGrid.instance.obs)
    //    {
    //        Vector3 diff = transform.position - position;
    //        float currentDis = diff.sqrMagnitude;
    //        if (currentDis < distance)
    //        {
    //            closest = obj;
    //            distance = currentDis;
    //            Debug.Log(closest);
    //        }
    //    }

    //    return closest;
    //}
    //public void FindClosestObstacle()
    //{
    //    float distance = Mathf.Infinity;
    //    Vector3 position = transform.position;
    //    foreach (GameObject obj in BuildingsGrid.instance.obs)
    //    {
    //        Vector3 diff = obj.transform.position - position;
    //        float currentDis = diff.sqrMagnitude;
    //        if (currentDis < distance)
    //        {
    //            closest = obj;
    //            distance = currentDis;
    //            Debug.Log(closest.transform.position);
    //            closest.transform.localScale = new Vector3(.5f, .5f, .5f);
                
    //        }
    //    }

    //    //return closest;
    //}
}

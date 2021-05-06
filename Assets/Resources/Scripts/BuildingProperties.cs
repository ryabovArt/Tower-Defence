using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingProperties : MonoBehaviour
{
    [SerializeField] private AssetItem item;

    [SerializeField] private GameObject buildingObj;
    public GameObject BuildingObj { get { return buildingObj; } }
    [SerializeField] private int buildingHealthState;
    public int BuildingHealthState { get { return buildingHealthState; } set { buildingHealthState = value; } }
    private void Start()
    {
        buildingObj = item.BuildingObject;
        buildingHealthState = item.BuildingHealth;
    }

    //void DestroyObstacle()
    //{
    //    if ((gameObject.transform.position - meshAgent.transform.position).magnitude < 1.5f)
    //    {
    //        if (gameObject.GetComponent<BuildingProperties>().BuildingHealthState <= 1)
    //        {
    //            foreach (var obs in PlacingBuilding.instance.Way)
    //            {
    //                if (Vector3.Distance(gameObject.transform.position, obs.transform.position) < 0.3f
    //                       && obs.tag == "FullPlace")
    //                {
    //                    obs.tag = "Way";
    //                }
    //            }

    //            PlacingBuilding.instance.buildObs.Remove(gameObject);
    //            Destroy(gameObject);
    //            target.position = originalTarget;
    //            text.text = string.Empty;
    //            hit = 0;
    //            isDestroy = false;
    //            //meshAgent.speed = 0.5f;
    //            //yield break;
    //        }

    //        text.text = $"Attack! {hit}";
    //        hit++;
    //    }
    //}
}

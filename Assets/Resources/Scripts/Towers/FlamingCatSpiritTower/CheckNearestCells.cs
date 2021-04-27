using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckNearestCells : MonoBehaviour
{
    public GameObject[] cell;
    private float distanceBeyweenCells;

    void Start()
    {
        distanceBeyweenCells = Vector3.Distance(cell[0].transform.position, cell[12].transform.position);

        for (int i = 0; i < cell.Length; i++)
        {
            if (Vector3.Distance(transform.position, cell[i].transform.position) < distanceBeyweenCells * 1.2f)
            {
                //Debug.Log(cell[i].name);
            }
        }
    }
}

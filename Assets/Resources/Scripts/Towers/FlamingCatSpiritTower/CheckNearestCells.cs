using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckNearestCells : MonoBehaviour
{
    [SerializeField] private GameObject[] map;
    [SerializeField] private List<Transform> cell = new List<Transform>();
    internal List<Transform> nearestCells = new List<Transform>();
    public List<Transform> NearestCells { get => nearestCells;/* set { nearestCells = value; }*/ }

    private float distanceBetweenCells;
    private TowerFieldOfView towerFieldOfView;

    private void OnEnable()
    {
        towerFieldOfView = GetComponentInChildren<TowerFieldOfView>();
        map = GameObject.FindGameObjectsWithTag("Map");

        for (int i = 0; i < map.Length; i++)
        {
            foreach (Transform child in map[i].GetComponentInChildren<Transform>())
            {
                cell.Add(child);
            }
        }
        
        //FindeNearestCells(cell);
        StartCoroutine(GetNearestCells());
    }

    IEnumerator GetNearestCells()
    {
        while (true)
        {
            if (PlacingBuilding.instance.FlyingBuilding == null)
                FindeNearestCells(cell);
            yield return new WaitForSeconds(1f);
        }
    }

    public void FindeNearestCells(List<Transform> cells)
    {
        distanceBetweenCells = Vector3.Distance(cells[0].transform.position, cells[1].transform.position);

        for (int i = 0; i < cells.Count; i++)
        {
            if (Vector3.Distance(transform.position, cells[i].transform.position) < distanceBetweenCells * 1.2f)
            {
                if (transform.position.x < cells[i].transform.position.x
                     && transform.position.z == cells[i].transform.position.z) // клетка справа
                {
                    //Debug.Log(cells[i].name);
                    nearestCells.Add(cells[i].transform);
                    TowerFieldOfView.sector = "right";
                    print("right" + cells[i].transform);
                }
                if (transform.position.x == cells[i].transform.position.x
                     && transform.position.z < cells[i].transform.position.z) // клетка сверху
                {
                    //Debug.Log(cell[i].name);
                    nearestCells.Add(cells[i].transform);
                    TowerFieldOfView.sector = "top";
                }
                if (transform.position.x > cells[i].transform.position.x
                     && transform.position.z == cells[i].transform.position.z) // клетка слева
                {
                    //Debug.Log(cells[i].name);
                    nearestCells.Add(cells[i].transform);
                    TowerFieldOfView.sector = "left";
                }
                if (transform.position.x == cells[i].transform.position.x
                     && transform.position.z > cells[i].transform.position.z) // клетка снизу
                {
                    //Debug.Log(cells[i].name);
                    nearestCells.Add(cells[i].transform);
                    TowerFieldOfView.sector = "bottom";
                }
            }
        }
        foreach (var item in nearestCells)
        {
            //print(item.localPosition);
        }
    }
}

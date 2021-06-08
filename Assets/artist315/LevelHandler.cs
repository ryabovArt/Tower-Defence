using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelHandler : Singleton<LevelHandler>
{
    public GameObject WallsParent;
    public GameObject BuildParent;
    public GameObject PathParent;

    [HideInInspector] public List<GameObject> Walls;
    [HideInInspector] public List<GameObject> Build;
    [HideInInspector] public List<GameObject> Path;
    [HideInInspector] public List<GameObject> Obst;
    void Start()
    {
        Walls = ArrayToList(WallsParent);
        Build = ArrayToList(BuildParent);
        Path = ArrayToList(PathParent);
        ChangeScene.OnLevelStarted += OnLevelStarted;
    }

    private List<GameObject> ArrayToList(GameObject parent)
    {
        List<GameObject> list = new List<GameObject>();
        for (int i = 0; i < parent.transform.childCount; i++)
        {
            list.Add(parent.transform.GetChild(i).gameObject);
        }
        return list;
    }

    private void OnLevelStarted()
    {
        var t = GameObject.FindGameObjectsWithTag("Obstacle");
        //Debug.Log(t.Length);
        foreach (var item in t)
        {
            Obst.Add(item);
        }
    }



}

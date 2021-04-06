using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesOnScene : MonoBehaviour
{
    public static EnemiesOnScene instance = null;

    public List<GameObject> enemiesOnScene = new List<GameObject>();

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }
}

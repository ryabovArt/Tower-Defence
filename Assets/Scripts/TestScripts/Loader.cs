using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loader : MonoBehaviour
{
    [SerializeField] private GameObject spawner;

    private void Awake()
    {
        if (Spawner.instance == null) Instantiate(spawner);
    }
}

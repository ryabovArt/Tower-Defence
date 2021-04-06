using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerTest : MonoBehaviour
{
    public TowersData towersData;

    void Start()
    {
        Debug.Log(towersData.name + towersData.armor + towersData.damage);
    }
}

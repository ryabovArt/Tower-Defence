using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemy
{
    string EnemyName { get; }
    GameObject EnemyObject { get; }
    int EnemyHealth { get; }
    int SpawnAmount { get; }
    float SpawnDelay { get; }
}

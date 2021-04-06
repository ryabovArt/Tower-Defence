using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item/Enemy")]
public class AssetEnemy : ScriptableObject, IEnemy
{
    public string EnemyName => enemyName;
    public GameObject EnemyObject => enemyObject;
    public int EnemyHealth => enemyHealth;
    public int SpawnAmount => spawnAmount;
    public float SpawnDelay => spawnDelay;

    [SerializeField] private string enemyName;
    [SerializeField] private GameObject enemyObject;
    [SerializeField] private int enemyHealth;
    [SerializeField] private int spawnAmount;
    [SerializeField] private float spawnDelay;
}

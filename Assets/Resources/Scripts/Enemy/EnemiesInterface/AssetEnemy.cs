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

    public float Damage => damage;

    public float Armour => armour;

    public float Speed => speed;

    public int Reward => reward;

    public float AttackRate => attackRate;

    //public int SpawnAmount => spawnAmount;
    //public float SpawnDelay => spawnDelay;

    [SerializeField] private string enemyName;
    [SerializeField] private GameObject enemyObject;
    [SerializeField] private int enemyHealth;
    [SerializeField] private float damage;
    [SerializeField] private float armour;
    [SerializeField] private float speed;
    [SerializeField] private int reward;
    [SerializeField] private float attackRate;
}

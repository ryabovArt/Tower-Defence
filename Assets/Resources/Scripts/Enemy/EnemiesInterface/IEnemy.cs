using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemy
{
    string EnemyName { get; }
    GameObject EnemyObject { get; }
    int EnemyHealth { get; }
    float Damage { get; }
    float Armour { get; }
    float Speed { get; }
    int Reward { get; }
    float AttackRate { get; }
}
//    int SpawnAmount { get; }
//    float SpawnDelay { get; }
//}

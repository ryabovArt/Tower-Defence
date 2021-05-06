using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TowersBehaviour : MonoBehaviour
{
    public Transform lookAtObject;
    public float damage;
    public GameObject bullet;
    [Header("Интервал атаки")]
    public float timeBetweenAttack;

    public abstract IEnumerator Attack();
}

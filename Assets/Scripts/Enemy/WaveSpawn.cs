using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawn : MonoBehaviour
{
    public static WaveSpawn instance = null;

    [SerializeField] private GameObject[] enemyPrefab;
    [SerializeField] private Transform[] spawnPoint;

    [Header("Спавн нового врага/сек")]
    [SerializeField] private float interval;
    [Header("Максимальное кол-во мобов на сцене")]
    [SerializeField] private int maxEnemiesOnScene;

    private int currentWaveSize = 0;

    public Transform target;

    // temp
    public Text text;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        StartCoroutine(EnemySpawn());
    }

    IEnumerator EnemySpawn()
    {
        if(maxEnemiesOnScene == currentWaveSize)
        {
            StopCoroutine(EnemySpawn());
        }
        else if(maxEnemiesOnScene > currentWaveSize)
        {
            int indexEnemy = Random.Range(0, enemyPrefab.Length);
            int indexPoint = Random.Range(0, spawnPoint.Length);
            GameObject enemy = Instantiate(enemyPrefab[indexEnemy], spawnPoint[indexPoint].position, Quaternion.identity) as GameObject;
            currentWaveSize++;
            yield return new WaitForSeconds(interval);
            StartCoroutine(EnemySpawn());
        }
        
    }
}

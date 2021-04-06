using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawn : MonoBehaviour
{
    public static WaveSpawn instance = null;

    [SerializeField] private AssetEnemy[] enemyPrefab;
    [SerializeField] private Transform[] spawnPoint;

    public Transform target;

    [SerializeField] private float betweenWaweDelayMain;
    private float betweenWaweDelay;

    private int currentEnemyPrefab = 0;
    private int spawned = 0;
    private float spawnDelayMain = 0;
    private float spawnDelay = 0;

    // temp
    public Text text;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        betweenWaweDelay = betweenWaweDelayMain;
        spawnDelayMain = enemyPrefab[currentEnemyPrefab].SpawnDelay;
    }

    private void Update()
    {
        betweenWaweDelay -= Time.deltaTime;

        if (betweenWaweDelay <= 0)
        {
            spawnDelay -= Time.deltaTime;

            if (spawnDelay <= 0 && spawned < enemyPrefab[currentEnemyPrefab].SpawnAmount)
            {
                Instantiate(enemyPrefab[currentEnemyPrefab].EnemyObject, spawnPoint[1]);
                spawned++;
                spawnDelay = spawnDelayMain;
            }
        }
        if (spawned >= enemyPrefab[currentEnemyPrefab].SpawnAmount && currentEnemyPrefab + 1 < enemyPrefab.Length)
        {
            currentEnemyPrefab++;
            spawned = 0;
            spawnDelayMain = enemyPrefab[currentEnemyPrefab].SpawnDelay;
            betweenWaweDelay = betweenWaweDelayMain;
        }
    }

    //[Header("Спавн нового врага/сек")]
    //[SerializeField] private float interval;
    //[Header("Максимальное кол-во мобов на сцене")]
    //[SerializeField] private int maxEnemiesOnScene;

    //private int currentWaveSize = 0;

    //private void Awake()
    //{
    //    if (instance == null) instance = this;
    //    else Destroy(gameObject);
    //}

    //private void Start()
    //{
    //    StartCoroutine(EnemySpawn());
    //}

    //IEnumerator EnemySpawn()
    //{
    //    if(maxEnemiesOnScene == currentWaveSize)
    //    {
    //        StopCoroutine(EnemySpawn());
    //    }
    //    else if(maxEnemiesOnScene > currentWaveSize)
    //    {
    //        int indexEnemy = Random.Range(0, enemyPrefab.Length);
    //        int indexPoint = Random.Range(0, spawnPoint.Length);
    //        GameObject enemy = Instantiate(enemyPrefab[indexEnemy].EnemyObject, spawnPoint[indexPoint].position, Quaternion.identity) as GameObject;
    //        currentWaveSize++;
    //        yield return new WaitForSeconds(interval);
    //        StartCoroutine(EnemySpawn());
    //    }

    //}
}

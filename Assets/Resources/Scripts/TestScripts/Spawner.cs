using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{
    public static Spawner instance = null;

    public Transform endPoint;
    public GameObject spawnPoint;
    //public List<EnemiesMover> EnemyList = new List<EnemiesMover>();
    [SerializeField] private GameObject[] enemy;

    [SerializeField] private int maxEnemiesOnScene;
    [SerializeField] private int totalEnemies;
    [SerializeField] private int enemiesPerSpawn;

    private int enemiesOnScene = 0;

    [Header("Спавн нового врага/сек")]
    [SerializeField] private float spawnDelay;

    // Временные переменные
    public Text txt;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        if(enemiesPerSpawn > 0 && enemiesOnScene < totalEnemies)
        {
            for (int i = 0; i < enemiesPerSpawn; i++)
            {
                if(enemiesOnScene < maxEnemiesOnScene)
                {
                    GameObject newEnemy = Instantiate(enemy[1]) as GameObject;
                    //newEnemy.transform.position = spawnPoint.transform.position;
                    enemiesOnScene++;
                }
            }
            yield return new WaitForSeconds(spawnDelay);
            StartCoroutine(Spawn());
        }
    }

    //public void RegisterEnemy(EnemiesMover enemy)
    //{
    //    EnemyList.Add(enemy);
    //}

    //public void UnregisterEnemy(EnemiesMover enemy)
    //{
    //    EnemyList.Remove(enemy);
    //    Destroy(enemy.gameObject);
    //}

    //public void DestroyEnemies()
    //{
    //    foreach(EnemiesMover enemy in EnemyList)
    //    {
    //        Destroy(enemy.gameObject);
    //    }

    //    EnemyList.Clear();
    //}
}

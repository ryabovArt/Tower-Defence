using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;

public class SpawnWaves : MonoBehaviour
{
   //public static SpawnWaves instance = null;

   // private void Awake()
   // {
   //     if (instance == null) instance = this;
   //     else Destroy(gameObject);
   // }

    //public Transform target;

    //// temp
    //public Text text;

    [System.Serializable]
    public class Wave
    {
        [Header("Название волны")]
        public string name;

        public SpawnPoint spawnPoint;
    }
    public Wave[] waves;

    [System.Serializable]
    public class SpawnPoint
    {
        [Header("Точка спавна")]
        public Transform spawnPoint;
        public Enemy[] enemy;
    }

    [System.Serializable]
    public class Enemy
    {
        [Header("Вид врага")]
        public AssetEnemy enemyPrefab;
        [Header("Время задержки между спавном врагов")]
        public float delay;
    }

    public enum SpawnState { SPAWNING, WAITING, COUNTING }; // состояния врагов

    private int currentWave;
    private float delaySpawnCorutine;

    [Header("Время перед началом волны")]
    public float timeBetweenWaves;
    public float waveCountdown;

    private float serchCountdown;

    private SpawnState state = SpawnState.COUNTING;

    private void Start()
    {
        waveCountdown = timeBetweenWaves;
    }

    private void Update()
    {
        if (StartWaves.isGenerateWaves)
        {
            if (state == SpawnState.WAITING) // если враги живы
            {
                if (!isEnemyAlive()) // действия если врагов нет
                {
                    WaveCompleted();
                }
                else return;
            }

            if (waveCountdown <= 0)
            {
                if (state != SpawnState.SPAWNING)
                {
                    if (waveCountdown != 0 && waves.Length >= currentWave)
                        StartCoroutine(SpawnWave());
                }
            }
            else
            {
                waveCountdown -= Time.deltaTime;
            }
        }
    }

    /// <summary>
    /// Завершение волн
    /// </summary>
    private void WaveCompleted()
    {
        state = SpawnState.COUNTING;
        waveCountdown = timeBetweenWaves;

        //if (currentWave + 1 > waves.Length - 1)
        //{
        //    currentWave = 0;
        //}
        ++currentWave;

        if (currentWave == waves.Length)
        {
            FindObjectOfType<ChangeScene>().Win();
            Destroy(gameObject);
        } 
    }

    /// <summary>
    /// Есть ли на сцене живые враги
    /// </summary>
    private bool isEnemyAlive()
    {
        serchCountdown -= Time.deltaTime;
        if (serchCountdown <= 0)
        {
            serchCountdown = 1f;
            if (GameObject.FindGameObjectWithTag("Enemy") == null)
            {
                return false;
            }
        }

        return true;
    }

    /// <summary>
    /// События, происходящие в текущей волне
    /// </summary>
    /// <returns></returns>
    IEnumerator SpawnWave()
    {        
        state = SpawnState.SPAWNING;

        for (int i = 0; i < waves[currentWave].spawnPoint.enemy.Length; i++)
        {
            SpawnEnemy(waves[currentWave].spawnPoint.enemy[i].enemyPrefab, waves[currentWave].spawnPoint.spawnPoint);

            delaySpawnCorutine = waves[currentWave].spawnPoint.enemy[i].delay;
            yield return new WaitForSeconds(delaySpawnCorutine);
        }

        state = SpawnState.WAITING;
        yield break;
    }
    
    /// <summary>
    /// Создание врага
    /// </summary>
    /// <param name="enemy">враги</param>
    /// <param name="point">точка спавна врага</param>
    private void SpawnEnemy(AssetEnemy enemy, Transform point)
    {
        Instantiate(enemy.EnemyObject, point.position, Quaternion.identity);
    }
}

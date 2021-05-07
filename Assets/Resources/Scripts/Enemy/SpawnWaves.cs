using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;

public class SpawnWaves : MonoBehaviour
{
   public static SpawnWaves instance = null;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    public Transform target;

    // temp
    public Text text;

    [System.Serializable]
    public class Wave
    {
        [Header("Название волны")]
        public string name;
        [Header("Время между спавном врагов (чем меньше значение, тем больше задержка)")]
        public float rate;
        [Header("Время задержки между спавном врагов")]
        public string timeDelay;
        [Header("Точка спавна")]
        public Transform spawnPoint;
        [Header("Вид врага")]
        public List<AssetEnemy> enemyPrefab = new List<AssetEnemy>();
    }
    public Wave[] waves; 

    public enum SpawnState { SPAWNING, WAITING, COUNTING }; // состояния врагов

    private int currentWave;
    private int enemyIndex;
    private float delaySpawnCorutine;

    [Header("Время перед началом волны")]
    public float timeBetweenWaves;
    public float waveCountdown;

    private float serchCountdown;

    private SpawnState state = SpawnState.COUNTING;

    private void Start()
    {
        waveCountdown = timeBetweenWaves;
        delaySpawnCorutine = 1f / waves[currentWave].rate;
    }

    private void Update()
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
                StartCoroutine(SpawnWave(waves[currentWave], waves[currentWave].enemyPrefab));
            }
        }
        else
        {
            waveCountdown -= Time.deltaTime;
        }
    }

    /// <summary>
    /// Завершение волн
    /// </summary>
    private void WaveCompleted()
    {
        state = SpawnState.COUNTING;
        waveCountdown = timeBetweenWaves;

        if (currentWave + 1 > waves.Length - 1)
        {
            currentWave = 0;
        }

        currentWave++;
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
    /// <param name="wave">текущая волна</param>
    /// <param name="enemy">враги</param>
    /// <returns></returns>
    IEnumerator SpawnWave(Wave wave, List<AssetEnemy> enemy)
    {
        state = SpawnState.SPAWNING;
        enemyIndex = 0;

        for (int i = 0; i < enemy.Count; i++)
        {
            if (enemy[i] == null && waves[currentWave].timeDelay != string.Empty)
            // берем первую цифру в строке и присваиваем ее переменной, отвечающей за задержку между спавном врагов
            {
                StringBuilder sb = new StringBuilder(waves[currentWave].timeDelay);
                char ch = sb[0];

                if (waves[currentWave].timeDelay.Length > 1)
                {
                    sb.Remove(0, 2);
                }

                enemy.Remove(enemy[i]);
                waves[currentWave].timeDelay = sb.ToString();
                
                delaySpawnCorutine = float.Parse(ch.ToString());
                yield return new WaitForSeconds(delaySpawnCorutine);
                delaySpawnCorutine = 1f / waves[currentWave].rate;
            }

            SpawnEnemy(wave.enemyPrefab, wave.spawnPoint);

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
    private void SpawnEnemy(List<AssetEnemy> enemy, Transform point)
    {

        if (enemyIndex <= enemy.Count)
        {
            Instantiate(enemy[enemyIndex].EnemyObject, point.position, Quaternion.identity);
            enemyIndex++;
        }
    }
}

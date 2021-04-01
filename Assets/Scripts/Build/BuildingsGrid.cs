using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class BuildingsGrid : MonoBehaviour
{
    public static BuildingsGrid instance = null;

    public Vector2Int gridSize = new Vector2Int(11, 11);

    public GameObject[] wall; //кубы из которых состоит путь
    private List<GameObject> way = new List<GameObject>(); //список кубов, по которым будет проходить путь
    private List<GameObject> towerPoint = new List<GameObject>(); //список кубов на которых стоят башни

    private Building[,] grid;
    private /*Building*/GameObject flyingBuilding; //строение в момент когда зажата ЛКМ
    private Camera mainCamera;

    internal List<GameObject> obs = new List<GameObject>(); //построенные препятствия

    [SerializeField] private Material wayMaterial; //материал кубов из которых состоит путь
    [SerializeField] private Material towerPlaceMaterial; //материал кубов на которые ставятся башни

    private bool isBuilt = false; // построена ли башня
    private Collider coll; // коллайдер строения

    private TowerTrigger towerTrigger; // ссылка на скрипт
    private TowerShoot towerShoot; // ссылка на скрипт
    NavMeshObstacle meshObstacle;
    int value = 0;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        grid = new Building[gridSize.x, gridSize.y];
        mainCamera = Camera.main;
        for (int i = 0; i < wall.Length; i++)
        {
            if (wall[i].tag == "Way")
            {
                way.Add(wall[i]);
                wall[i].GetComponent<Renderer>().material = wayMaterial;
            }
            else if(wall[i].tag == "TowerPlace")
            {
                towerPoint.Add(wall[i]);
                wall[i].GetComponent<Renderer>().material = towerPlaceMaterial;
            }
        }
    }

    void Update()
    {
        if (flyingBuilding != null)
        {
            MovingFlyingBuilding();
            DeactivateTowerProperties();
        }
        else
        {
            ActivateTowerProperties(value);
        }
        
    }

    /// <summary>
    /// Перемещение строения по сцене за курсором
    /// </summary>
    private void MovingFlyingBuilding()
    {
        var groundPlane = new Plane(Vector3.up, Vector3.zero);
        var ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        if (groundPlane.Raycast(ray, out float position))
        {
            Vector3 worldPos = ray.GetPoint(position);

            int xPos = Mathf.RoundToInt(worldPos.x);
            int yPos = Mathf.RoundToInt(worldPos.z);

            bool isAvailable = true;

            if (xPos < 0 || xPos >= 11) isAvailable = false;
            if (yPos < 0 || yPos >= 11) isAvailable = false;

            flyingBuilding.transform.position = new Vector3(xPos, 0, yPos);

            if (isAvailable && Input.GetMouseButtonUp(0))
            {
                SearchPlaceForBuilding();
            }
        }
    }

    /// <summary>
    /// Поиск места для строения
    /// </summary>
    private void SearchPlaceForBuilding()
    {
        if (SelectTower.isTowerSelected == false)
        {
            foreach (var obs in way)
            {
                if (Vector3.Distance(flyingBuilding.transform.position, obs.transform.position) < 0.3f
                    && obs.tag != "FullPlace")
                {
                    flyingBuilding = null;
                    obs.tag = "FullPlace";
                    return;
                }
            }
        }
        else
        {
            foreach (var twr in towerPoint)
            {
                if (Vector3.Distance(flyingBuilding.transform.position, twr.transform.position) < 0.3f
                    && twr.tag != "FullPlace")
                {
                    flyingBuilding = null;
                    twr.tag = "FullPlace";
                    SelectTower.isTowerSelected = false;
                    return;
                }
            }
        }
    }

    /// <summary>
    /// Добавляем объект в массив построенных объектов
    /// </summary>
    public void AddObstacle()
    {
        obs.Add(flyingBuilding);
    }

    /// <summary>
    /// Ставим строение
    /// </summary>
    /// <param name="building"> объект строения </param>
    public void StartPlacingBuilding(GameObject/*Building*/ building)
    {
        if (flyingBuilding != null)
            Destroy(flyingBuilding);

        flyingBuilding = Instantiate(building);
    }

    /// <summary>
    /// Деактивируем свойства башни для того, чтобы она
    /// не реагировала на противников в момент перемещения за курсором перед постройкой
    /// </summary>
    private void DeactivateTowerProperties()
    {
        if (!isBuilt)
        {
            coll = flyingBuilding.GetComponent<BoxCollider>();
            coll.isTrigger = true;

            if (flyingBuilding.CompareTag("Tower"))
            {
                towerTrigger = flyingBuilding.GetComponent<TowerTrigger>();
                towerTrigger.enabled = false;
                towerShoot = flyingBuilding.GetComponent<TowerShoot>();
                towerShoot.isShoot = true;
                value = 1;
            }
            else if (flyingBuilding.CompareTag("Obstacle"))
            {
                meshObstacle = flyingBuilding.GetComponent<NavMeshObstacle>();
                meshObstacle.enabled = false;
                value = 2;
            }

            isBuilt = true;
            
        }
        
    }

    /// <summary>
    /// Активируем свойства башни после постройки
    /// </summary>
    private void ActivateTowerProperties(int val)
    {
        if (isBuilt)
        {
            if(val == 1)
            {
                coll.isTrigger = false;
                towerTrigger.enabled = true;
                towerShoot.isShoot = false;
            }
            if (val == 2) meshObstacle.enabled = true;

            isBuilt = false;
        }
    }
}

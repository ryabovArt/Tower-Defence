using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class PlacingBuilding: MonoBehaviour
{
    public static PlacingBuilding instance = null;

    public Vector2Int gridSize = new Vector2Int(21, 15);

    [SerializeField] private GameObject[] wall; //кубы из которых состоит путь
    private List<GameObject> way = new List<GameObject>(); //список кубов, по которым будет проходить путь
    public List<GameObject> Way
    {
        get { return way; }
        set { way = value; }
    }
    private List<GameObject> towerPoint = new List<GameObject>(); //список кубов на которых стоят башни

    private DrawBuildingGrid[,] grid;
    private GameObject flyingBuilding; //строение в момент когда нажата ЛКМ
    public GameObject FlyingBuilding
    {
        get { return flyingBuilding; }
        set { flyingBuilding = value; }
    }

    private Camera mainCamera;
    internal Animator towerAnimator;

    internal List<GameObject> buildObs = new List<GameObject>(); //построенные препятствия
    private GameObject temp = null; // объект для добавления препятствия в список построенных

    [SerializeField] private Material wayMaterial; //материал кубов из которых состоит путь
    [SerializeField] private Material towerPlaceMaterial; //материал кубов на которые ставятся башни
    [SerializeField] private Material canPlaceBuildingMaterial; //материал кубов из которых состоит путь

    public bool isBuilt = false; // построена ли башня
    private Collider coll; // коллайдер строения

    private TowerTrigger towerTrigger; // ссылка на скрипт
    private TowerShoot towerShoot; // ссылка на скрипт
    NavMeshObstacle meshObstacle;

    private GameObject tempCell;
    private int value = 0;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        //manaScript = FindObjectOfType<ManaScript>();
        ChangeScene.OnLevelStarted += OnLevelStarted;
        //towerTrigger = GameObject.FindGameObjectWithTag("GameController").GetComponent<TowerTrigger>();
        grid = new DrawBuildingGrid[gridSize.x, gridSize.y];
        mainCamera = Camera.main;
        for (int i = 0; i < wall.Length; i++)
        {
            if (wall[i].tag == "Way")
            {
                way.Add(wall[i]);
                wall[i].GetComponent<Renderer>().material = wayMaterial;
            }
            else if (wall[i].tag == "TowerPlace")
            {
                towerPoint.Add(wall[i]);
                wall[i].GetComponent<Renderer>().material = towerPlaceMaterial;
            }
        }
    }

    private void OnLevelStarted()
    {
        buildObs = LevelHandler.Instance.Obst;
    }

    void Update()
    {
        if (flyingBuilding != null)
        {
            MovingFlyingBuilding();
            //DeactivateBuildingProperties();
        }
        //else if (StartWaves.isGenerateWaves == true)
        //{
        //    //ActivateBuildingProperties(value);
        //}
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
            //print(flyingBuilding.name);

            if (!isAvailable && Input.GetMouseButtonUp(0))
            {
                Destroy(flyingBuilding.gameObject);
                //SearchPlaceForBuilding();
            }
        }
    }

    /// <summary>
    /// Ставим строение
    /// </summary>
    /// <param name="building"> объект строения </param>
    public void StartPlacingBuilding(GameObject building)
    {
        if (flyingBuilding != null)
            Destroy(flyingBuilding);

        flyingBuilding = Instantiate(building);
    }

    /// <summary>
    /// Поиск места для строения
    /// </summary>
    public void EndPlacingBuilding()
    {
        foreach (var element in wall)
        {
            if (Vector3.Distance(flyingBuilding.transform.position, element.transform.position) < 0.3f)
            {
                if (BuildingsCell.isTowerSelected == false)
                {
                    //var t = flyingBuilding.GetComponent<TowersBehaviour>().buildingCost;
                    if (element.CompareTag("Way") && !element.CompareTag("FullPlace"))
                    {
                        var t = flyingBuilding.GetComponent<TowersBehaviour>().buildingCost;
                        if (ManaScript.Instance.Build(t))
                        {
                            //Debug.Log(ManaScript.Instance.mana);
                            flyingBuilding = null;
                            element.tag = "FullPlace";
                            return;
                        }
                        else
                        {
                            Destroy(flyingBuilding.gameObject);
                        }
                    }
                    else if (!element.CompareTag("Way") || element.CompareTag("FullPlace"))
                    {
                        Destroy(flyingBuilding.gameObject);
                    }
                }
                else
                {
                    if (!element.CompareTag("Way") && !element.CompareTag("FullPlace"))
                    {

                        //Debug.Log(manaScript.mana);
                        //Debug.Log(manaScript.Build(t));
                        var t = flyingBuilding.GetComponent<TowersBehaviour>().buildingCost;
                        if (ManaScript.Instance.Build(t))
                        {
                            flyingBuilding = null;
                            element.tag = "FullPlace";
                            return;
                        }
                        else
                        {
                            Destroy(flyingBuilding.gameObject);
                        }

                    }
                    else if (element.CompareTag("Way") || element.CompareTag("FullPlace"))
                    {
                        Destroy(flyingBuilding.gameObject);
                    }
                }
            }
        }
    }

    /// <summary>
    /// Подсвечиваем клетку
    /// </summary>
    public void CellsHighlighting()
    {
        if (BuildingsCell.isTowerSelected == false)
        {
            foreach (var obs in Way) obs.GetComponent<Renderer>().material.color = Color.green;
        }
        else
        {
            foreach (var twr in towerPoint) twr.GetComponent<Renderer>().material.color = Color.green;
        }
    }

    /// <summary>
    /// Убираем подсветку клеток
    /// </summary>
    public void RemoveCellsHighlighting()
    {
        if (BuildingsCell.isTowerSelected == false)
        {
            foreach (var obs in Way) obs.GetComponent<Renderer>().material.color = wayMaterial.color;
        }
        else
        {
            foreach (var twr in towerPoint) twr.GetComponent<Renderer>().material.color = towerPlaceMaterial.color;
        }
    }
}

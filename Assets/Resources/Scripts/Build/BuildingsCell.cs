using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class BuildingsCell : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Text nameField; // название строения
    [SerializeField] private Button icon; // кнопка строения
    [SerializeField] private Image imageIcon; // картинка строения
    [SerializeField] private GameObject go; // объект строения
    public GameObject GO { get { return go; } }
    [SerializeField] private Image buildingDiscriptionImage;
    [SerializeField] private GameObject buildingDiscriptionToHide;
    [SerializeField] private TextMeshProUGUI buildingDiscriptionText;

    private Transform _transformParent;
    private Transform _transformOriginal;

    public static bool isTowerSelected = false; // выбрана башня или другое строение
    private Animator animator;
    private TowerTrigger towerTrigger;
    //public List<GameObject> constructedObjects = new List<GameObject>();
    public Dictionary<int, GameObject> constructedObjects = new Dictionary<int, GameObject>();
    int index;

    private void OnEnable()
    {
        animator = go.GetComponent<Animator>();
    }

    public void Init(Transform transformParent)
    {
        _transformParent = transformParent;
        _transformOriginal = transform.parent;
    }

    /// <summary>
    /// Рендерим кнопку с башней
    /// </summary>
    /// <param name="item"> текущий айтем со строением </param>
    public void Render(ITower item)
    {
        nameField.text = item.Name;
        imageIcon.sprite = item.BuildingImage;
        go = item.BuildingObject;
        buildingDiscriptionImage.sprite = item.TowerDiscriptionImage;
        buildingDiscriptionText.text = item.TowerDiscriptionText;
    }

    /// <summary>
    /// Действия при клике на айтем со строением
    /// </summary>
    public void TowerChoose()
    {
        //PlacingBuilding.instance.StartPlacingBuilding(GO);
        //if (GO.CompareTag("Obstacle")) isTowerSelected = false;
        //else if (GO.CompareTag("Tower")) isTowerSelected = true;       
    }


    public void OnDrag(PointerEventData eventData)
    {
        //GO.transform.position = Input.mousePosition;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        PlacingBuilding.instance.StartPlacingBuilding(GO);
        //constructedObjects.Add(index, GO);
        //animator = GO.GetComponent<Animator>();
        //animator.enabled = false;
        //print("animator" + animator.enabled);
        if (GO.CompareTag("Obstacle")) isTowerSelected = false;
        else if (GO.CompareTag("Tower")) isTowerSelected = true;
        PlacingBuilding.instance.CellsHighlighting();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        PlacingBuilding.instance.EndPlacingBuilding();
        PlacingBuilding.instance.RemoveCellsHighlighting();
        //FindObjectOfType<ManaScript>().Build();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        print("OnPointerEnter");
        buildingDiscriptionToHide.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        print("OnPointerExit");
        buildingDiscriptionToHide.SetActive(false);
    }
}

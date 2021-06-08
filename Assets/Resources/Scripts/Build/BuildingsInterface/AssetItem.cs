using System;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Item/Building")]
public class AssetItem : ScriptableObject, ITower
{
    // инкапсулируем свойства айтемов
    public string Name => name;
    public Button UIButton => uiButton;
    public Sprite BuildingImage => buildingImage;
    public GameObject DragBuilding => dragBuilding;
    public GameObject BuildingObject => buildingObject;
    //public int BuildingCost => buildingCost;
    //public int BuildingHealth => buildingCost;

    int ITower.BuildingHealth { get => BuildingHealth; set => throw new NotImplementedException(); }
    public Sprite TowerDiscriptionImage => towerDiscriptionImage;

    public string TowerDiscriptionText => towerDiscriptionText;

    [SerializeField] private new string name;
    [SerializeField] private Button uiButton;
    [SerializeField] private Sprite buildingImage;
    [SerializeField] private GameObject dragBuilding;
    [SerializeField] private GameObject buildingObject;
    //[SerializeField] private int buildingCost;
    public int BuildingHealth;
    [SerializeField] private Sprite towerDiscriptionImage;
    [Header("Описание башни")]
    [TextArea]
    [SerializeField] private string towerDiscriptionText;
}

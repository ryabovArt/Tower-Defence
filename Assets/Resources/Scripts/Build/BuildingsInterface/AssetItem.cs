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
    public GameObject BuildingObject => buildingObject;

    public int BuildingHealth => buildingHealth;

    [SerializeField] private new string name;
    [SerializeField] private Button uiButton;
    [SerializeField] private Sprite buildingImage;
    [SerializeField] private GameObject buildingObject;
    [SerializeField] private int buildingHealth;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BuildingsCell : MonoBehaviour
{
    [SerializeField] private Text nameField; // название строения
    [SerializeField] private Button icon; // кнопка строения
    [SerializeField] private Image imageIcon; // картинка строения
    [SerializeField] private GameObject go; // объект строения
    public GameObject GO { get { return go; } }

    public static bool isTowerSelected = false; // выбрана башня или другое строение

    /// <summary>
    /// Рендерим кнопку с башней
    /// </summary>
    /// <param name="item"> текущий айтем со строением </param>
    public void Render(ITower item)
    {
        nameField.text = item.Name;
        imageIcon.sprite = item.BuildingImage;
        go = item.BuildingObject;
    }

    /// <summary>
    /// Действия при клике на айтем со строением
    /// </summary>
    public void TowerChoose()
    {
        PlacingBuilding.instance.StartPlacingBuilding(GO);
        if (GO.CompareTag("Obstacle")) isTowerSelected = false;
        else if (GO.CompareTag("Tower")) isTowerSelected = true;           
    }
}

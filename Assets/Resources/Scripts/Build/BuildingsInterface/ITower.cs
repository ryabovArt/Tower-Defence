using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface ITower
{
    string Name { get; }
    Button UIButton { get; }
    Sprite BuildingImage { get; }
    GameObject DragBuilding { get; }
    GameObject BuildingObject { get; }
    //int BuildingCost { get; }
    int BuildingHealth { get; set; }
    Sprite TowerDiscriptionImage { get; }
    string TowerDiscriptionText { get; }
}

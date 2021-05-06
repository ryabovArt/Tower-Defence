using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface ITower
{
    string Name { get; }
    Button UIButton { get; }
    Sprite BuildingImage { get; }
    GameObject BuildingObject { get; }
    int BuildingHealth { get; set; }
}

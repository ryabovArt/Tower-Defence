using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Towers/Tower", fileName = "New Tower")]
public class TowersData : ScriptableObject
{
    public new string name;
    public Sprite towerSprite;
    public int damage;
    public int armor;
}

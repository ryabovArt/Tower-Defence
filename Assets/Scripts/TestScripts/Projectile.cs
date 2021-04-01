using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum projectileType { laserRed, laserBlue};

public class Projectile : MonoBehaviour
{
    [Header("Урон от снаряда")]
    [SerializeField] private int damage;

    [SerializeField] private projectileType pType;

    public int Damage
    {
        get
        {

            return damage;
        }
    }
    public projectileType PType
    {
        get
        {

            return pType;
        }
    }
}

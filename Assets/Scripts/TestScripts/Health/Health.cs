using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Health : MonoBehaviour
{
    [Header("Общее здоровье")]
    public int health;

    [Header("Наносимый урон")]
    public int getDamage;
    //[Header("Получаемый урон")]
    //public int setDamage;

    /// <summary>
    /// Наносимый урон
    /// </summary>
    public abstract void GetDamage();
    //public abstract void SetDamage();
}

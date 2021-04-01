using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectTower : MonoBehaviour
{
    private TowerBtn towerBtnPressed;
    public static bool isTowerSelected = false;

    void Update()
    {
        //Debug.Log(isTowerSelected);
    }

    /// <summary>
    /// Определяем, какая башня выбрана
    /// </summary>
    /// <param name="towerSelected"> выбранная башня </param>
    public void SelectedTower(TowerBtn towerSelected)
    {
        towerBtnPressed = towerSelected;
        isTowerSelected = true;
        Debug.Log(towerBtnPressed.name);
    }

    public void DeselectedTower()
    {
        isTowerSelected = false;
    }
}

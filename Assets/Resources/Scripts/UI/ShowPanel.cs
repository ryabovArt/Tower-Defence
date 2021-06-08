using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowPanel : MonoBehaviour
{
    [SerializeField] private GameObject enemiesList;

    public void ShowList()
    {
        enemiesList.SetActive(true);
    }

    public void HideList()
    {
        enemiesList.SetActive(false);
    }
}

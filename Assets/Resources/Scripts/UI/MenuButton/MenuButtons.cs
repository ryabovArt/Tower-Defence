using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MenuButtons : MonoBehaviour
{
    [SerializeField] private GameObject settingsPanel;

    public void TurnOnSettings()
    {
        settingsPanel.SetActive(true);
    }

    public void TurnOffSettings()
    {
        settingsPanel.SetActive(false);
    }

    public void Quit()
    {
        print("Quit");
        //Application.Quit();
    }
}

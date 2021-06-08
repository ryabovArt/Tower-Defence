using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartWaves : MonoBehaviour
{
    public static bool isGenerateWaves = false;

    [SerializeField] private GameObject[] spawnPoints;
    [SerializeField] private Animator buildingsPanelAnimator;

    /// <summary>
    /// Начало спавна мобов
    /// </summary>
    public void GenerateWaves()
    {
        isGenerateWaves = true;
        foreach (var point in spawnPoints)
        {
            point.SetActive(true);
        }
        buildingsPanelAnimator.SetTrigger("HidePanel");
    }
}

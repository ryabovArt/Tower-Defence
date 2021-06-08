using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidePanel : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    
    public void Hide()
    {
        panel.SetActive(false);
    }
}

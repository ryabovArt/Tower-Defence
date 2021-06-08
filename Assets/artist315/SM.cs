using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SM : MonoBehaviour
{
    [SerializeField] private GameObject LooseScreen;
    [SerializeField] private GameObject WinScreen;
    [SerializeField] private List<GameObject> spawns;
    [SerializeField] private string menuPath;
    private int count;
    private void Awake()
    {
        Time.timeScale = 1;
        var t = FindObjectsOfType<SpawnWaves>();
        Debug.Log(t.Length);
        foreach (var item in t)
        {
            spawns.Add(item.gameObject);
        }
        count = spawns.Count;
    }
    public void Win()
    {
        Debug.Log("Win");
        spawns.RemoveAll(delNul);
        count--;
        if (count == 0)
        {
            Time.timeScale = 0;
            WinScreen.SetActive(true);
        }
        
    }

    private static bool delNul(GameObject s)
    {
        return s == null;
    }

    public void Loose()
    {
        Time.timeScale = 0;
        LooseScreen.SetActive(true);
    }

    public void ToMenu()
    {
        SceneManager.LoadScene(menuPath, LoadSceneMode.Single);
    }
}

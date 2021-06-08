using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : Singleton<ChangeScene>
{
    [SerializeField] private GameObject LooseScreen;
    [SerializeField] private GameObject WinScreen;
    [SerializeField] private List<GameObject> spawns;
    [SerializeField] private int levelToLoad;
    [SerializeField] private Animator animator;
    private int count;
    //[HideInInspector] public bool isRunning = false;
    public delegate void ClickAction();
    public static event ClickAction OnLevelStarted;
    private void Start()
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
            OnPause();
            WinScreen.SetActive(true);
        }
        
    }

    private static bool delNul(GameObject s)
    {
        return s == null;
    }

    public void Lose()
    {
        Time.timeScale = 0;
        LooseScreen.SetActive(true);
    }

    public void FadeToLevel()
    {
        animator.SetTrigger("Fade");
        StartCoroutine(FadeComplete());
    }

    IEnumerator FadeComplete()
    {
        yield return new WaitForSeconds(0.9f);
        StartWaves.isGenerateWaves = false;
        SceneManager.LoadScene(levelToLoad);
    }

    public void ToMenu()
    {

        StartWaves.isGenerateWaves = false;
        SceneManager.LoadScene(levelToLoad);
    }

    public void OnPlay()
    {
        if (OnLevelStarted != null)
        {
            OnLevelStarted();
            Debug.Log("Starts");
        }
        //isRunning = true;
    }

    public void OnPause()
    {
        //isRunning = false;
    }


}

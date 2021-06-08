using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaSphereHandler : MonoBehaviour
{
    private bool isRunning = false;
    [SerializeField] private float minTime;
    [SerializeField] private float maxTime;
    [SerializeField] private int[] manaOrHealthIncome = {0,0 };
    [SerializeField] private int healthIncome;
    [SerializeField] private Vector3 yOffset;
    //[SerializeField] private ManaSphere t;

    [SerializeField] private GameObject manaSphere;
    // Start is called before the first frame update
    void Start()
    {
        ChangeScene.OnLevelStarted += OnLevelStarted;
    }

    // Update is called once per frame
    void Update()
    {
        
        
        //if (ChangeScene.Instance.isRunning)
        //{
        //    Debug.Log(isRunning);
        //    if (!isRunning)
        //    {
        //        isRunning = true;
        //        StartCoroutine(SpawnSphere(minTime, maxTime));
        //    }
        //}
    }

    private IEnumerator SpawnSphere(float min,float max)
    {
        Debug.Log("Starts");
        while (isRunning)
        {
            //Debug.Log("SpawnSphere");
            yield return new WaitForSeconds(Random.Range(min, max));
            int r = Random.Range(0, LevelHandler.Instance.Path.Count);
            GameObject pos = LevelHandler.Instance.Path[r];
            var s = Instantiate(manaSphere, pos.transform.position + yOffset, Quaternion.Euler(Vector3.zero));
            int rand = Random.Range(0,2);
            if (rand == 0)
            {
                s.GetComponent<ManaSphere>().ManaIncome = manaOrHealthIncome[0];
                s.GetComponent<ManaSphere>().HealthIncome = 0;
                Debug.Log("Mana");
            }
            else
            {
                s.GetComponent<ManaSphere>().ManaIncome =0;
                s.GetComponent<ManaSphere>().HealthIncome = manaOrHealthIncome[1];
                Debug.Log("Health");
            }
            
        }
    }

    private void OnLevelStarted()
    {
        Debug.Log("Starts");
        isRunning = true;
        StartCoroutine(SpawnSphere(minTime, maxTime));
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject spawnObject;
    private Vector3 spawnPos;
    private GameObject forDestroy;

    void Start()
    {
        spawnPos = new Vector3(-44.8f, 0.5f, Random.Range(-15f, -25f));
        StartCoroutine(SpawnObjCorutine());
    }

    IEnumerator SpawnObjCorutine()
    {
        yield return new WaitForSeconds(7f);
        Instantiate(spawnObject, spawnPos);
        yield return new WaitForSeconds(8f);
        Destroy(forDestroy);
    }

    private void Instantiate(GameObject spawnObject, Vector3 spawnPos)
    {
        forDestroy = Instantiate(spawnObject, spawnPos, Quaternion.identity);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCheckDist : MonoBehaviour
{
    public GameObject go1;
    public GameObject go2;

    // Update is called once per frame
    void Update()
    {
        Debug.Log(Vector3.Distance(go1.transform.position, go2.transform.position));
    }
}
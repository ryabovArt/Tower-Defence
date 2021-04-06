using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Test : MonoBehaviour
{
    private Camera camera;
    private NavMeshAgent meshAgent;

    private void Start()
    {
        camera = Camera.main;
        meshAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            if(Physics.Raycast(camera.ScreenPointToRay(Input.mousePosition), out hit))
            {
                meshAgent.SetDestination(hit.point);
            }
        }
    }

    //private Rigidbody rb;
    //private Transform transform;
    //private Vector3 dir, pos;

    //public float speed;

    //void Start()
    //{
    //    rb = GetComponent<Rigidbody>();
    //    transform = GetComponent<Transform>();
    //    pos = transform.position;
    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    dir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
    //    pos += dir;
    //}

    //private void FixedUpdate()
    //{
    //    rb.MovePosition(pos);
    //}
}

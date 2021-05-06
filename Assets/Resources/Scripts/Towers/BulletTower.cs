using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BulletTower : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float radius;

    [SerializeField] private float force;
    internal Transform target;

    List<GameObject> go = new List<GameObject>();

    void Update()
    {
        if(target)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            transform.LookAt(target);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Destroy(gameObject);
            //Explode();
        }
    }

    public void Explode()
    {
        StopCoroutine(GetComponent<EnemiesMover>().UpdateDestination());
        Collider[] overlappedColliders = Physics.OverlapSphere(transform.position, radius);
        List<GameObject> go = new List<GameObject>();

        for (int i = 0; i < overlappedColliders.Length; i++)
        {
            Rigidbody rb = overlappedColliders[i].attachedRigidbody;
            if (rb)
            {
                go.Add(overlappedColliders[i].gameObject);
            }
            
            
            //Rigidbody rb = overlappedColliders[i].attachedRigidbody;
            //if (rb)
            //{
            //    rb.AddExplosionForce(force, transform.position, radius);
            //}
        }
        
        
        foreach (var item in go)
        {
            //Debug.Log(item.transform.position);
            item.GetComponent<NavMeshAgent>().enabled = false;
            item.GetComponent<EnemiesMover>().enabled = false;
        }
        StartCoroutine(Navmesh());
    }
    IEnumerator Navmesh()
    {
        yield return new WaitForSeconds(1f);
        
        foreach (var item in go)
        {
            //Debug.Log(item.transform.position);
            item.GetComponent<NavMeshAgent>().enabled = true;
            item.GetComponent<EnemiesMover>().enabled = true;
        }
        StartCoroutine(GetComponent<EnemiesMover>().UpdateDestination());
    }
}

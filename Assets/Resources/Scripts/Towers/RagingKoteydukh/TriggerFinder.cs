using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerFinder : MonoBehaviour
{
    //public Animator towerAnimator;
    private GameObject currentTarget;
    private bool lockEnemy;
    [SerializeField]private List<GameObject> enemies = new List<GameObject>();

    private enum State { PATROL, ATTACK };

    private State state = State.PATROL;
    private State lastState = State.PATROL;
    [SerializeField]private TowerShoot towerShoot;

    private void Start()
    {
        towerShoot = GetComponent<TowerShoot>();
        
    }

    private void Update()
    {
        if (enemies.Count == 0)
        {
            lockEnemy = !lockEnemy;
            state = State.PATROL;
        }
        else
        {
            state = State.ATTACK;
            
        }
        //SetAnimation();
    }

    public GameObject FindClosest()
    {
        int num = 0;
        float len = CalcualteDist(enemies[0],this.gameObject);

        for (int i = 0; i < enemies.Count; i++)
        {
            float newLen = CalcualteDist(enemies[i], this.gameObject);
            if (len > newLen)
            {
                len = newLen;
                num = i;
            }
        }
        return enemies[num];
    }

    public GameObject FindFurthest()
    {
        int num = 0;
        float len = CalcualteDist(enemies[0], this.gameObject);

        for (int i = 0; i < enemies.Count; i++)
        {
            float newLen = CalcualteDist(enemies[i], this.gameObject);
            if (len < newLen)
            {
                len = newLen;
                num = i;
            }
        }
        return enemies[num];
    }

    public GameObject FindFirst()
    {
        return enemies[0];
    }

    public GameObject FindRandom()
    {
        int i = UnityEngine.Random.Range(0, enemies.Count);
        return enemies[i];
    }

    public GameObject AskForTarget(TargetTypes key)
    {
        GameObject target = null;
        switch (key)
        {
            case TargetTypes.Closest:
                target = FindClosest();
                break;
            case TargetTypes.furthest:
                target = FindFurthest();
                break;
            case TargetTypes.random:
                target = FindRandom();
                break;
            default:
                break;
        }
        return target;
    }



    private float CalcualteDist(GameObject g1, GameObject g2)
    {
        return (g1.transform.position - g2.transform.position).magnitude;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemies.Add(other.gameObject);
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy")/* && other.gameObject == currentTarget*/)
        {
            enemies.Remove(other.gameObject);
            //towerShoot.target = null;
            currentTarget = null;
            lockEnemy = !lockEnemy;
        }
    }

    //private void SetAnimation()
    //{
    //    switch (state)
    //    {
    //        case State.PATROL:
    //            //Debug.Log("patrol");
    //            towerAnimator.enabled = true;
    //            towerAnimator.Play("Patrol");
    //            break;
    //        case State.ATTACK:
    //            //Debug.Log("attack");
    //            towerAnimator.enabled = false;
    //            break;
    //    }
    //}

    //private void SetState(State to)
    //{
    //    lastState = state;
    //    state = to;
    //}

}

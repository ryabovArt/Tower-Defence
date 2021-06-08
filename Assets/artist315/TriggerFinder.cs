using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerFinder : MonoBehaviour
{
    public Animator towerAnimator;
    private GameObject currentTarget;
    private bool lockEnemy;
    [SerializeField]private List<GameObject> enemies = new List<GameObject>();

    private enum State { PATROL, ATTACK };

    //private State state = State.PATROL;

    private void Update()
    {
        if (enemies.Count == 0)
        {
            lockEnemy = !lockEnemy;
            //state = State.PATROL;
        }
        else
        {
            //state = State.ATTACK;
        }
        ListChek();

        //SetAnimation();
    }

    private void ListChek()
    {
        enemies.RemoveAll(IsEmpty);
    }

    private static bool IsEmpty(GameObject enemy)
    {
        return enemy == null;
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
        if (enemies.Count != 0)
        {
            switch (key)
            {
                case TargetTypes.Closest:
                    target = FindClosest();
                    break;
                case TargetTypes.Furtherest:
                    target = FindFurthest();
                    break;
                case TargetTypes.Random:
                    target = FindRandom();
                    break;
                default:
                    break;
            }
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

    public bool IsEmpty()
    {
        return enemies.Count == 0;
    }

    //private void SetAnimation()
    //{
    //    switch (state)
    //    {
    //        case State.PATROL:
    //            //Debug.Log("patrol");
    //            if (StartWaves.isGenerateWaves)
    //            {
    //                towerAnimator.enabled = true;
    //                towerAnimator.Play("Patrol");
    //            }
    //            break;
    //        case State.ATTACK:
    //            //Debug.Log("attack");
    //            towerAnimator.enabled = false;
    //            break;
    //    }
    //}
}

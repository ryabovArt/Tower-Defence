using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerTrigger : MonoBehaviour
{
    public Animator towerAnimator;
    private GameObject currentTarget;
    private bool lockEnemy;

    private enum State { PATROL, ATTACK};
    private State state = State.PATROL;
    private State lastState = State.PATROL;

    private void Update()
    {
        if(!currentTarget)
        {
            lockEnemy = !lockEnemy;
            state = State.PATROL;
        }
        SetAnimation();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Enemy") && !lockEnemy)
        {
            GetComponent<TowerShoot>().target = other.gameObject.transform;
            currentTarget = other.gameObject;
            lockEnemy = true;
            state = State.ATTACK;
            //Debug.Log(state);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy")/* && other.gameObject == currentTarget*/)
        {
            GetComponent<TowerShoot>().target = null;
            currentTarget = null;
            lockEnemy = !lockEnemy;
        }
    }

    /// <summary>
    /// Анимация башен
    /// </summary>
    private void SetAnimation()
    {
        switch(state)
        {
            case State.PATROL:
                //Debug.Log("patrol");
                towerAnimator.enabled = true;
                towerAnimator.Play("Patrol");
                break;
            case State.ATTACK:
                //Debug.Log("attack");
                towerAnimator.enabled = false;
                break;
        }
    }

    private void SetState(State to)
    {
        lastState = state;
        state = to;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerTrigger : MonoBehaviour
{
    public Animator towerAnimator;
    private GameObject currentTarget;
    public Transform fieldOfWiew; // трансформ зоны видимости для того, чтобы враг был в пределах сектора
    public Transform turrel; // трансформ башни для того, чтобы она всегда смотрела на врага
    private bool lockEnemy;
    private SphereCollider collider;

    private enum State { PATROL, ATTACK };
    private State state = State.PATROL;

    private void Start()
    {
        collider = GetComponent<SphereCollider>();
    }

    private void Update()
    {
        if (!currentTarget)
        {
            lockEnemy = !lockEnemy;
            state = State.PATROL;
        }
        SetAnimation();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Enemy") && !lockEnemy && !PlacingBuilding.instance.FlyingBuilding)
        {
            //GetComponent<TowerShoot>().target = other.gameObject.transform;
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
            //GetComponent<TowerShoot>().target = null;
            currentTarget = null;
            lockEnemy = !lockEnemy;
        }
    }

    /// <summary>
    /// Анимация башен
    /// </summary>
    private void SetAnimation()
    {
        switch (state)
        {
            case State.PATROL:
                if (StartWaves.isGenerateWaves)
                {
                    //Debug.Log("patrol");
                    towerAnimator.enabled = true;
                    towerAnimator.Play("Patrol");
                }
                break;
            case State.ATTACK:
                //Debug.Log("attack");
                //fieldOfWiew.LookAt(currentTarget.transform);
                turrel.LookAt(currentTarget.transform);
                towerAnimator.enabled = false;
                break;
        }
    }
}

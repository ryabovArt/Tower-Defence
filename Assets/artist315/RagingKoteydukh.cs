using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagingKoteydukh : TowersBehaviour
{
    [SerializeField] private Transform shootElement;
    [SerializeField]internal Transform target;
    private TriggerFinder triggerFinder;
    public TargetTypes behavior = TargetTypes.Closest;     //отвечает за то какую цель будет выбирать скрипт
    internal bool isShoot = false;

    private enum State { PATROL, ATTACK };
    private State state = State.PATROL;
    private State lastState = State.PATROL;
    private Animator towerAnimator;
    [Header("Пареметры Ускорения")]
    [SerializeField] private float a;
    [SerializeField] private float b;
    [SerializeField] private float step = 1;
    [SerializeField] private float currDmg;
    [SerializeField] private float maxDmg = 40;
    [SerializeField] private string str;

    private GameObject prevTarget;
    private float time;
    private void Start()
    {
        currDmg = damage;
        triggerFinder = GetComponent<TriggerFinder>();
        towerAnimator = GetComponent<Animator>();
    }
    void Update()
    {
        if (!triggerFinder.IsEmpty())
        {
            state = State.ATTACK;
            target = triggerFinder.AskForTarget(behavior).transform;
            if (target.gameObject.name != str)
            {
                currDmg = damage;
                time = timeBetweenAttack;
            }
                
            lookAtObject.transform.LookAt(target);
            if (!isShoot)
            {
                
                if (target.gameObject.name != str)
                {
                    
                    
                }
                if (currDmg < maxDmg)
                {

                    currDmg += step;
                }
                time = a * currDmg * currDmg + b;
                StartCoroutine(Attack());
            }
        }
        else
        {
            state = State.PATROL;
        }
        SetAnimation();
    }

    public override IEnumerator Attack()
    {
        isShoot = true;
        GameObject blt = Instantiate(bullet, shootElement.position, Quaternion.identity) as GameObject;
        
        blt.GetComponent<BulletTower>().target = target;
        blt.GetComponent<BulletTower>().Damage = currDmg;
        blt.GetComponent<BulletTower>().damageType = DamageType;

        yield return new WaitForSeconds(time);
        isShoot = !isShoot;
    }

    private void SetAnimation()
    {
        switch (state)
        {
            case State.PATROL:
                if(StartWaves.isGenerateWaves)
                {
                    towerAnimator.enabled = true;
                    towerAnimator.Play("Patrol");
                }
                break;
            case State.ATTACK:
                towerAnimator.enabled = false;
                break;
        }
    }
}

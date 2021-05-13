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
    }
    void Update()
    {
        target = triggerFinder.AskForTarget(behavior).transform;

        if (target)
        {
            if (target.gameObject.name != str)
            {
                currDmg = damage;
                time = timeBetweenAttack;
                Debug.Log(target.gameObject.name + "   " + str);
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
        //prevTarget.name = target.gameObject.name;
        str = target.gameObject.name;
    }

    public override IEnumerator Attack()
    {
        //Debug.Log(bullet);
        isShoot = true;
        GameObject blt = Instantiate(bullet, shootElement.position, Quaternion.identity) as GameObject;
        
        blt.GetComponent<BulletTower>().target = target;

        yield return new WaitForSeconds(time);

        if (!target) Destroy(blt.gameObject);
        isShoot = !isShoot;
    }
}

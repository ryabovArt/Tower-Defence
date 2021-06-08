using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StormyKoteydukh : TowersBehaviour
{

    [SerializeField] private Transform shootElement;
    [SerializeField] internal Transform target;
    [SerializeField] internal Vector3 attackPos = new Vector3(0,5,0);
    private TriggerFinder triggerFinder;
    public TargetTypes behavior = TargetTypes.Closest;     //отвечает за то какую цель будет выбирать скрипт
    internal bool isShoot = false;


    // Start is called before the first frame update
    void Start()
    {
        triggerFinder = GetComponent<TriggerFinder>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!triggerFinder.IsEmpty())
        {
            target = triggerFinder.AskForTarget(behavior).transform;
            if (!isShoot)
            {
                StartCoroutine(Attack());
            }
        }            
    }

    public override IEnumerator Attack()
    {
        isShoot = true;
        GameObject blt = Instantiate(bullet, target.position + attackPos, Quaternion.identity) as GameObject;
        target.gameObject.GetComponent<Enemy>().RecieveDamage(damage,DamageType);
        blt.GetComponent<BulletTower>().target = target;
        blt.GetComponent<BulletTower>().Damage = 0;
        yield return new WaitForSeconds(timeBetweenAttack);
        isShoot = !isShoot;
    }
}

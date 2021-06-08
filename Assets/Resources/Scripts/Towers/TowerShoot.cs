using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TowerFieldOfView))]
public class TowerShoot : TowersBehaviour
{
    private TowerFieldOfView tower;

    [SerializeField] private Transform shootElement; // стрелюящая часть башни

    [Header("Интервал удара")]
    [SerializeField] private float timeBetweenShoot;

    internal Transform target;
    internal bool isShoot;
    GameObject blt;

    private void Start()
    {
        tower = GetComponent<TowerFieldOfView>();
    }

    void Update()
    {
        if (!isShoot)
        {
            StartCoroutine(Attack());
        }
    }

    /// <summary>
    /// Атака
    /// </summary>
    /// <returns></returns>
    public override IEnumerator Attack()
    {
        StartCoroutine(Shoot());

        yield return new WaitForSeconds(timeBetweenAttack);

        isShoot = !isShoot;
    }

    /// <summary>
    /// Удар
    /// </summary>
    /// <returns></returns>
    private IEnumerator Shoot()
    {
        int i = 0;
        while (i < 2)
        {
            isShoot = true;

            foreach (var visiblrTarget in tower.visibleTargets)
            {
                if (visiblrTarget != null)
                {
                    blt = GameObject.Instantiate(bullet, shootElement.position, Quaternion.identity) as GameObject;
                    blt.GetComponent<BulletTower>().target = visiblrTarget;
                    visiblrTarget.GetComponent<EnemyBasicClass>().RecieveDamage(damage);
                    Debug.Log(visiblrTarget.gameObject.GetComponent<Enemy>());
                    //target.gameObject.GetComponent<Enemy>().RecieveDamage(damage);
                    if (visiblrTarget == null) Destroy(blt.gameObject);
                }
            }
            yield return new WaitForSeconds(0.5f);
            i++;
        }
    }
}

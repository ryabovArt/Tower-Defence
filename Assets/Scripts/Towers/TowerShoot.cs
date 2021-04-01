using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerShoot : TowersBehaviour
{
    [SerializeField] private Transform shootElement; // стрелюящая часть башни

    internal Transform target;
    internal bool isShoot;

    void Update()
    {
        if(target)
        {
            lookAtObject.transform.LookAt(target);
            if (!isShoot)
            {
                StartCoroutine(Shoot());
            }
        }
    }

    /// <summary>
    /// Выстрел
    /// </summary>
    /// <returns></returns>
    public override IEnumerator Shoot()
    {
        isShoot = true;
        GameObject blt = GameObject.Instantiate(bullet, shootElement.position, Quaternion.identity) as GameObject;
        //Debug.Log(bullet);
        blt.GetComponent<BulletTower>().target = target;

        yield return new WaitForSeconds(timeBetweenShoot);

        if (!target) Destroy(blt.gameObject);
        isShoot = !isShoot;
    }
}

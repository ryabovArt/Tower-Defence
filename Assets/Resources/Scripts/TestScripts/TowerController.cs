//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class TowerController : MonoBehaviour
//{
//    [SerializeField] private float timeBetweenAttack; // время между атаками
//    [SerializeField] private float attackRadius; // радиус атаки
//    [SerializeField] private Projectile projectile; // вид снаряда

//    private EnemiesMover targetEnemy = null; // &&&
//    private float attackCounter;

//    private bool isAttack = false;

//    void Start()
//    {
        
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        attackCounter -= Time.deltaTime;

//        if(targetEnemy == null)
//        {
//            EnemiesMover nearestEnemy = GetNearestEnemy();
//            if (nearestEnemy != null 
//                && Vector3.Distance(transform.localPosition, nearestEnemy.transform.localPosition) <= attackRadius)
//            {
//                targetEnemy = nearestEnemy;
//            }
//        }
//        else
//        {
//            if(attackCounter <= 0)
//            {
//                isAttack = true;

//                attackCounter = timeBetweenAttack;
//            }
//            else
//            {
//                isAttack = false;
//            }

//            if (Vector3.Distance(transform.localPosition, targetEnemy.transform.localPosition) > attackRadius)
//            {
//                targetEnemy = null;
//            }
//        }
//    }

//    public void FixedUpdate()
//    {
//        if(isAttack == true)
//        {
//            Attack();
//        }
//    }

//    public void Attack()
//    {
//        isAttack = false;
//        Projectile newProjectile = Instantiate(projectile) as Projectile;
//        newProjectile.transform.localPosition = transform.localPosition;
         
//        if(targetEnemy == null)
//        {
//            Destroy(newProjectile);
//        }
//        else
//        {
//            StartCoroutine(MoveProjectile(newProjectile));
//        }
//    }

//    IEnumerator MoveProjectile(Projectile projectile)
//    {
//        while (GetTargetDistance(targetEnemy) > 0.2f && projectile != null && targetEnemy != null)
//        {
//            var dir = targetEnemy.transform.localPosition - transform.localPosition;
//            var angleDirection = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
//            projectile.transform.rotation = Quaternion.AngleAxis(angleDirection, Vector3.forward);
//            projectile.transform.localPosition = Vector3.MoveTowards(projectile.transform.localPosition, targetEnemy.transform.localPosition, 5f * Time.deltaTime);
//            yield return null;
//        }

//        if(projectile != null || targetEnemy == null)
//        {
//            Destroy(projectile);
//        }
//    }

//    private float GetTargetDistance(EnemiesMover thisEnemy)
//    {
//        if(thisEnemy == null)
//        {
//            thisEnemy = GetNearestEnemy();
//            if (thisEnemy == null) return 0f;
//        }
        
//        return Mathf.Abs(Vector3.Distance(transform.localPosition, thisEnemy.transform.localPosition));
//    }

//    private List<EnemiesMover> GetEnemiesInRange()
//    {
//        List<EnemiesMover> enemiesInRange = new List<EnemiesMover>();

//        foreach (EnemiesMover enemy in Spawner.instance.EnemyList)
//        {
//            if (Vector3.Distance(transform.localPosition, enemy.transform.localPosition) <= attackRadius)
//            {
//                enemiesInRange.Add(enemy);
//            }
//        }

//        return enemiesInRange;
//    }

//    private EnemiesMover GetNearestEnemy()
//    {
//        EnemiesMover nearestEnemy = null;
//        float smallestDistance = float.PositiveInfinity;
        
//        foreach (EnemiesMover enemy in GetEnemiesInRange() )
//        {
//            if (Vector3.Distance(transform.localPosition, enemy.transform.localPosition) < smallestDistance)
//            {
//                smallestDistance = Vector3.Distance(transform.localPosition, enemy.transform.localPosition);
//                nearestEnemy = enemy;
//            }
//        }
//        return nearestEnemy;
//    }
//}

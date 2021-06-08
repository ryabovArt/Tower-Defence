using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerFieldOfView : MonoBehaviour
{
    [Header("Радиус видимости мобов")]
    public float viewRadius;

    [Header("Угол видимости мобов сверху")]
    [Range(0, 360)]
    public float viewAngle;

    [Header("Угол видимости мобов снизу")]
    [Range(0, 360)]
    public float viewAngle_2;

    [Header("Угол видимости мобов слева")]
    [Range(0, 360)]
    public float viewAngle_3;

    [Header("Угол видимости мобов справа")]
    [Range(0, 360)]
    public float viewAngle_4;

    public LayerMask targetMask;
    public LayerMask obstacleMask;

    internal List<Transform> visibleTargets = new List<Transform>();
    //
    private Transform attackedMob;
    public bool isChooseAttackedMob = false;
    private CheckNearestCells neareCells;
    public static string sector = string.Empty;

    private void Start()
    {
        neareCells = GetComponentInParent<CheckNearestCells>();
        StartCoroutine(FindTargetsWithDelay(0.2f));

    }

    IEnumerator FindTargetsWithDelay(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            FindVisibleTargets();
        }
    }

    /// <summary>
    /// Определяем мобов в зоне видимости башни
    /// </summary>
    private void FindVisibleTargets()
    {
        visibleTargets.Clear();
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);

        GetAttackedEnemy(targetsInViewRadius);

        AttackEnemiesInSector(targetsInViewRadius);
    }

    /// <summary>
    /// Находим атакуемого врага
    /// </summary>
    /// <param name="targets">враги в зоне видимости</param>
    private void GetAttackedEnemy(Collider[] targets)
    {
        if (attackedMob == null || Vector3.Distance(transform.position, attackedMob.position) > viewRadius)
        {
            isChooseAttackedMob = false;
        }
        if (!isChooseAttackedMob && targets.Length > 0)
        {
            attackedMob = GetClosestEnemy(targets);
            //attackedMob.GetComponent<EnemiesMover>().sp = 0.3f;
            isChooseAttackedMob = true;
        }
    }

    /// <summary>
    /// Атакуем выбранного врага и врагов, находящихся с ним в одном секторе
    /// </summary>
    /// <param name="targets">враги в зоне видимости</param>
    private void AttackEnemiesInSector(Collider[] targets)
    {
        for (int i = 0; i < targets.Length; i++)
        {
            if (attackedMob != null && Vector3.Distance(transform.position, attackedMob.position) < viewRadius)
            {
                Transform target = targets[i].transform;

                Vector3 directionToTarget = (target.position - transform.position).normalized;

                Transform currentClosestCell = GetClosestCell(neareCells.NearestCells);

                Debug.DrawLine(currentClosestCell.position, attackedMob.position, Color.blue, 3f);

                if (Vector3.Distance(attackedMob.position, currentClosestCell.position) < 1.2f && attackedMob != null)
                {
                    if (currentClosestCell.position.x < transform.position.x
                     && currentClosestCell.position.z == transform.position.z) // клетка слева
                    {
                        LeftSector(transform.right, directionToTarget, target);
                        print("left");
                    }

                    if (currentClosestCell.position.x == transform.position.x
                         && currentClosestCell.position.z > transform.position.z) // клетка сверху
                    {
                        TopSector(transform.forward, directionToTarget, target);
                        print("up");
                    }

                    if (currentClosestCell.position.x < transform.position.x
                             && currentClosestCell.position.z == transform.position.z) // клетка справа
                    {
                        RightSector(transform.right, directionToTarget, target);
                        print("right");
                    }

                    if (currentClosestCell.position.x == transform.position.x
                             && currentClosestCell.position.z < transform.position.z) // клетка снизу
                    {
                        BottomSector(-transform.forward, directionToTarget, target);
                        print("bottom");
                    }
                }
            }
        }
    }

    /// <summary>
    /// Получаем ближайшего к башне врага
    /// </summary>
    /// <param name="enemies">враги в зоне видимости</param>
    /// <returns>ближайший к башне враг</returns>
    Transform GetClosestEnemy(Collider[] enemies)
    {
        Transform tMin = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = transform.position;
        foreach (Collider col in enemies)
        {
            float dist = Vector3.Distance(col.transform.position, currentPos);
            if (dist < minDist)
            {
                tMin = col.transform;
                minDist = dist;
            }
        }
        return tMin;
    }

    /// <summary>
    /// Получаем ближайшие к атакуемому врагу ячейки
    /// </summary>
    /// <param name="enemies">все ячейки</param>
    /// <returns>ближайшая ячейка</returns>
    Transform GetClosestCell(List<Transform> cells)
    {
        Transform tMin = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = attackedMob.transform.position;
        foreach (Transform t in cells)
        {
            float dist = Vector3.Distance(t.position, currentPos);
            if (dist < minDist)
            {
                tMin = t;
                minDist = dist;
            }
        }
        return tMin;
    }

    /// <summary>
    /// Направление от угла
    /// </summary>
    /// <param name="angleInDegrees"> угол в градусах </param>
    /// <param name="isGlobalAngle"> является ли угол глобальным </param>
    /// <returns></returns>
    public Vector3 DirectionFromAngle (float angleInDegrees, bool isGlobalAngle)
    {
        if (!isGlobalAngle)
        {
            angleInDegrees += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }

    /// <summary>
    /// Мобы из верхнего сектора
    /// </summary>
    /// <param name="tr"> направление </param>
    /// <param name="dirToTarget"> расстояние </param>
    /// <param name="trg"> моб </param>
    private void TopSector(Vector3 tr, Vector3 dirToTarget, Transform trg)
    {
        if (Vector3.Angle(tr, dirToTarget) < viewAngle / 2)
        {
            float distanceToTarget = Vector3.Distance(transform.position, trg.position);

            if (!Physics.Raycast(transform.position, dirToTarget, distanceToTarget, obstacleMask))
            {
                visibleTargets.Add(trg);
            }
        }
    }

    /// <summary>
    /// Мобы из нижнего сектора
    /// </summary>
    /// <param name="tr"> направление </param>
    /// <param name="dirToTarget"> расстояние </param>
    /// <param name="trg"> моб </param>
    private void BottomSector(Vector3 tr, Vector3 dirToTarget, Transform trg)
    {
        if (Vector3.Angle(tr, dirToTarget) < viewAngle_2 / 2)
        {
            float distanceToTarget = Vector3.Distance(transform.position, trg.position);

            if (!Physics.Raycast(transform.position, dirToTarget, distanceToTarget, obstacleMask))
            {
                visibleTargets.Add(trg);
            }
        }
    }


    /// <summary>
    /// Мобы из правого сектора
    /// </summary>
    /// <param name="tr"> направление </param>
    /// <param name="dirToTarget"> расстояние </param>
    /// <param name="trg"> моб </param>
    private void RightSector(Vector3 tr, Vector3 dirToTarget, Transform trg)
    {
        if (Vector3.Angle(tr, dirToTarget) > viewAngle_3 / 2)
        {
            float distanceToTarget = Vector3.Distance(transform.position, trg.position);

            if (!Physics.Raycast(transform.position, dirToTarget, distanceToTarget, obstacleMask))
            {
                visibleTargets.Add(trg);
            }
        }
    }

    /// <summary>
    /// Мобы из левого сектора
    /// </summary>
    /// <param name="tr"> направление </param>
    /// <param name="dirToTarget"> расстояние </param>
    /// <param name="trg"> моб </param>
    private void LeftSector(Vector3 tr, Vector3 dirToTarget, Transform trg)
    {
        if (Vector3.Angle(tr, dirToTarget) > viewAngle_4 / 2)
        {
            float distanceToTarget = Vector3.Distance(transform.position, trg.position);

            if (!Physics.Raycast(transform.position, dirToTarget, distanceToTarget, obstacleMask))
            {
                visibleTargets.Add(trg);
            }
        }
    }
}

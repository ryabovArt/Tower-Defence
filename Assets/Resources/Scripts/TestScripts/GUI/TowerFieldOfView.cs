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

    public List<Transform> visibleTargets = new List<Transform>();

    private void Start()
    {
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

    private void FindVisibleTargets()
    {
        visibleTargets.Clear();
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);

        for (int i = 0; i < targetsInViewRadius.Length; i++)
        {
            Debug.Log(targetsInViewRadius[i].name);
            Transform target = targetsInViewRadius[i].transform;

            Vector3 directionToTarget = (target.position - transform.position).normalized;

            // Добавляем в массив мобов в пределах зон видимости 
            TopSector(transform.forward, directionToTarget, target);

            BottomSector(-transform.forward, directionToTarget, target);

            RightSector(transform.right, directionToTarget, target);

            LeftSector(-transform.right, directionToTarget, target);
        }
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

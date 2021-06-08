using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor (typeof (TowerFieldOfView))]
public class TowerFieldOfViewEditior : Editor
{
    /// <summary>
    /// Рисуем окружность, углы и линии до мобов в пределах видимости
    /// </summary>
    private void OnSceneGUI()
    {
        TowerFieldOfView towerField = (TowerFieldOfView)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(towerField.transform.position, Vector3.up, Vector3.forward, 360, towerField.viewRadius);
        Vector3 viewAngleA = towerField.DirectionFromAngle(towerField.viewAngle / 2, false);
        Vector3 viewAngleB = towerField.DirectionFromAngle(-towerField.viewAngle / 2, false);

        Vector3 viewAngleC = towerField.DirectionFromAngle(towerField.viewAngle_2 / 2, false);
        Vector3 viewAngleD = towerField.DirectionFromAngle(-towerField.viewAngle_2 / 2, false);
        
        Vector3 viewAngleE = towerField.DirectionFromAngle(towerField.viewAngle_3 / 2 - 90f, false);
        Vector3 viewAngleF = towerField.DirectionFromAngle(-towerField.viewAngle_3 / 2 - 90f, false);

        Vector3 viewAngleG = towerField.DirectionFromAngle(-towerField.viewAngle_4 / 2 + 90f, false);
        Vector3 viewAngleH = towerField.DirectionFromAngle(towerField.viewAngle_4 / 2 + 90f, false);


        Handles.DrawLine(towerField.transform.position, towerField.transform.position + viewAngleA * towerField.viewRadius);
        Handles.DrawLine(towerField.transform.position, towerField.transform.position + viewAngleB * towerField.viewRadius);

        Handles.DrawLine(towerField.transform.position, towerField.transform.position - viewAngleC * towerField.viewRadius);
        Handles.DrawLine(towerField.transform.position, towerField.transform.position - viewAngleD * towerField.viewRadius);

        Handles.DrawLine(towerField.transform.position, towerField.transform.position - viewAngleE * towerField.viewRadius);
        Handles.DrawLine(towerField.transform.position, towerField.transform.position - viewAngleF * towerField.viewRadius);

        Handles.DrawLine(towerField.transform.position, towerField.transform.position - viewAngleG * towerField.viewRadius);
        Handles.DrawLine(towerField.transform.position, towerField.transform.position - viewAngleH * towerField.viewRadius);

        Handles.color = Color.red;
        foreach (var visibleTarget in towerField.visibleTargets)
        {
            if (visibleTarget != null)
            {
                Handles.DrawLine(towerField.transform.position, visibleTarget.position);
            }
        }
    }
}

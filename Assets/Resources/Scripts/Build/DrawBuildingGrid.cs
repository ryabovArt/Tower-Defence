using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawBuildingGrid : MonoBehaviour
{
    public Vector2Int size = Vector2Int.one;

    /// <summary>
    /// Рисуем сетку
    /// </summary>
    private void OnDrawGizmosSelected()
    {
        for (int x = 0; x < size.x; x++)
        {
            for (int y = 0; y < size.y; y++)
            {
                Gizmos.color = new Color(255, 138, 217);
                Gizmos.DrawCube(transform.position + new Vector3(x, -0.4f, y), new Vector3(1, 1, 1));
            }
        }
    }
}

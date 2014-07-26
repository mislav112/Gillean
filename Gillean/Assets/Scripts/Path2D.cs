using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEditor;

public class Path2D : MonoBehaviour
{

    public Transform[] Points;


    public IEnumerator<Transform> GetPathPointsEnumerator()
    {
        var direction = 1;
        var index = 0;

        if (Points == null || Points.Length < 1)
            yield break;

        while (true)
        {
            yield return Points[index];

            if (index <= 0)
                direction = 1;
            else if (index >= (Points.Length - 1))
                direction = -1;

            index += direction;
        }

    }

    public void OnDrawGizmos()
    {
        if (Points.Length < 2)
        {
            return;
        }

        for (var i = 1; i < Points.Length ; i++)
        {
            Gizmos.DrawLine(Points[i - 1].position, Points[i].position);
        }

    }    
}

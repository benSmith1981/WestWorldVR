using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct StartEndPoint
{
    public StartEndPoint(Transform startPoint, Transform endPoint)
    {
        StartPoint = startPoint;
        EndPoint = endPoint;
    }

    public Transform StartPoint { get; }
    public Transform EndPoint { get; }

    // public override string ToString() => $"({X}, {Y})";
}
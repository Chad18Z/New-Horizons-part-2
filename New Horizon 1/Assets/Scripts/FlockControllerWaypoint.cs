using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class FlockControllerWaypoint : MonoBehaviour
{
    [HideInInspector]
    public Vector2 Position;
    public int Order;

    [Tooltip("Time in seconds the flock will wait at the PREVIOUS waypoint")]
    public float WaitTime = 0f;

    private void Start()
    {
        Position = transform.position;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class FlockManager
{
    private List<FlockControllerWaypoint> waypoints;
    int currentWaypointIndex = 0;

    public void AddWaypoint(FlockControllerWaypoint waypoint)
    {
        if (!waypoints.Contains<FlockControllerWaypoint>(waypoint))
        {
            waypoints.Add(waypoint);
            waypoints.Sort((x, y) => x.Order.CompareTo(y.Order));
        }
        else
        {
            Debug.Log("Duplicate waypoint");
        }
    }

    public FlockControllerWaypoint GetNextWaypoint()
    {
        FlockControllerWaypoint w = waypoints[currentWaypointIndex];
        currentWaypointIndex++;
        if (currentWaypointIndex >= waypoints.Count)
        {
            currentWaypointIndex = 0;
        }
        
        return w;
    }

    public FlockManager()
    {
        waypoints = new List<FlockControllerWaypoint>();
    }
}

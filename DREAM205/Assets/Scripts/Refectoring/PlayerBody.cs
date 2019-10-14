using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerBody : MonoBehaviour
{
    private const string WAYPOINT_TAG = "WayPoint";
    public Action<Waypoint> OnMovedPlayer { get; set; }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(WAYPOINT_TAG))
        {
            Waypoint hitWaypoint = other.GetComponent<Waypoint>();
            if (hitWaypoint == null)
                return;

            if (OnMovedPlayer != null)
                OnMovedPlayer(hitWaypoint);
        }
    }
}

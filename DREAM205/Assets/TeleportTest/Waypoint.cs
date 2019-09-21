using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    //public Action<E_WaypointType> OnInActive { get; set; }
    Collider col;
    public List<Waypoint> connectedWayPointList = new List<Waypoint>();

    [SerializeField] bool isStartPoint;
    public bool IsStartPoint { get { return isStartPoint; } }

    private void Awake()
    {
        col = GetComponent<Collider>();
    }

    public void ActivateCollider(bool isActive)
    {
        if (col == null)
        {
            Debug.LogError(transform.name + "'s col is null");
            return;
        }

        col.enabled = isActive;
    }

}

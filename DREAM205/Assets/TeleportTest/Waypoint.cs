using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum E_WaypointType
{
    None,
    First,
    Second,
    Third,
    Fourth,
    Fifth,
    Sixth,
    Seventh,
    Eighth,
    Nineth,
    Tenth,
}

public class Waypoint : MonoBehaviour
{
    //public Action<E_WaypointType> OnInActive { get; set; }
    public E_WaypointType WayPointType { get { return wayPointType; } }

    [SerializeField] E_WaypointType wayPointType;
    Collider col;

    private void Awake()
    {
        col = GetComponent<Collider>();
    }

    public void ActivateCollider(bool isActive)
    {
        if (col == null)
        {
            Debug.LogError(wayPointType + " collider is null!");
            return;
        }

        col.enabled = isActive;
    }

}

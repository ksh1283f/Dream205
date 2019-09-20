using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum E_UserEventType
{
    None,
    LaserInteraction,
    OpenDoor,
    PickUp,
    LaserSelect,
}

public class UserEvent : MonoBehaviour
{
    public E_UserEventType UserEventType=E_UserEventType.None;
    public int EventID = -1;
    public bool IsEventEnd { get; private set; }

    /// <summary>
    /// 예정된 이벤트가 끝났을때만 호출
    /// </summary>
    /// <param name="col"></param>
    protected virtual void OnTriggerEnter(Collider col)
    {
        IsEventEnd = true;
    }
}

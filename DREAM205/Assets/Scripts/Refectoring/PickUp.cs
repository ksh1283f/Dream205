using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : UserEvent
{
    private void Awake()
    {
        UserEventType = E_UserEventType.PickUp;
    }

    protected override void OnTriggerEnter(Collider col)
    {
        // 집어올리는 작업

        // 예정된 작업이 모두 끝났을때 호출
        base.OnTriggerEnter(col);
    }
}

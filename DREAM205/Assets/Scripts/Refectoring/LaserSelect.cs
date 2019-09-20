using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserSelect : UserEvent
{
    private void Awake()
    {
        UserEventType = E_UserEventType.LaserSelect;
    }

    protected override void OnTriggerEnter(Collider col)
    {
        // 레이저 맞추는 작업

        // 예정된 작업이 모두 끝났을때 호출
        base.OnTriggerEnter(col);
    }
}

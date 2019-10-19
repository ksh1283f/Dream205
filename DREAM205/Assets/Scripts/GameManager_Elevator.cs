using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager_Elevator : MonoBehaviour
{
    public GameObject secondFloor;
    public GameObject fifthFloor;
    public GameObject fourteen;
    public GameObject closed;
    public SoundFadeEffect elevatorAmbience;

    public int FloorSelectedDataIndex;  // 인스펙터에서 초기화

    private void Awake()
    {
        closed.GetComponent<InteractableObj>().OnExecuteInteract += CloseButtonOrder;
        secondFloor.gameObject.GetComponent<InteractableObj>().OnExecuteInteract += FloorButtonOrder;
        fifthFloor.gameObject.GetComponent<InteractableObj>().OnExecuteInteract = FloorButtonOrder;
        fourteen.gameObject.GetComponent<InteractableObj>().OnExecuteInteract = FloorButtonOrder;

        closed.gameObject.GetComponent<BoxCollider>().enabled = false;
        secondFloor.gameObject.GetComponent<BoxCollider>().enabled = false;
        fifthFloor.gameObject.GetComponent<BoxCollider>().enabled = false;
        fourteen.gameObject.GetComponent<BoxCollider>().enabled = false;

        SoundManager.Instance.OnSoundEnd += AmbienceFadeOutToMiddle;
        SoundManager.Instance.OnSoundPlayEnd += AmbienceFadeOutToEnd;
    }

    private void CloseButtonOrder()
    {
        closed.gameObject.GetComponent<BoxCollider>().enabled = true;
    }

    private void FloorButtonOrder()
    {
        secondFloor.gameObject.GetComponent<BoxCollider>().enabled = true;
        fifthFloor.gameObject.GetComponent<BoxCollider>().enabled = true;
        fourteen.gameObject.GetComponent<BoxCollider>().enabled = true;
    }

    void AmbienceFadeOutToMiddle(int index)
    {
        if (index != FloorSelectedDataIndex)
            return;

        if(elevatorAmbience == null)
        {
            Debug.LogError("elevatorAmbience is null");
            return;
        }

        DirectingData data = SoundManager.Instance.DataList[index + 1];
        elevatorAmbience.SetFadeDuration(data.delayTime);
        elevatorAmbience.SetEffectType(E_EffectType.DecreaseFromOneToMiddle);
        StartCoroutine(elevatorAmbience.FadeEffect());
    }

    void AmbienceFadeOutToEnd()
    {
        if (elevatorAmbience == null)
        {
            Debug.LogError("elevatorAmbience is null");
            return;
        }

        elevatorAmbience.SetFadeDuration(1.5f);
        elevatorAmbience.SetEffectType(E_EffectType.DecreaseFromMiddleToZero);
        StartCoroutine(elevatorAmbience.FadeEffect());
    }
}

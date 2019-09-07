using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR;

public class TestFadeIn : MonoBehaviour
{
    [SerializeField] Text titleText;
    [SerializeField] Image titleBackground;

    public SteamVR_Action_Boolean trigger; // 트리거 입력 관련 변수
    public SteamVR_Action_Boolean grab;    // 그랩 입력 관련 변수

    bool isDirecting = false;
    float minFadeValue = 0f;
    float maxFadeValue = 1f;
    float fadeDuration = 2f;

    IEnumerator FadeInDirecting()
    {
        if (titleText == null || titleBackground==null)
            yield break;

        isDirecting = true;

        // 연출작업
        Color textColor = titleText.color;
        Color backgroundColor = titleBackground.color;
        float startTime = 0f;
        textColor.a = Mathf.Lerp(maxFadeValue, minFadeValue , startTime);
        backgroundColor.a = Mathf.Lerp(maxFadeValue, minFadeValue, startTime);
        while (textColor.a > minFadeValue || backgroundColor.a > minFadeValue)
        {
            startTime += Time.deltaTime / fadeDuration;
            textColor.a = Mathf.Lerp(maxFadeValue, minFadeValue, startTime);
            backgroundColor.a = Mathf.Lerp(maxFadeValue, minFadeValue, startTime);
            titleText.color = textColor;
            titleBackground.color = backgroundColor;
            Debug.LogError("directing..");
            yield return null;
        }

        titleBackground.gameObject.SetActive(false);
        titleText.gameObject.SetActive(false);
    }

    void Update()
    {
        // todo 키입력 받으면 연출 실행   
        if (IsCheckedInput())
        {
            if (isDirecting)
                return;

            StartCoroutine(FadeInDirecting());
        }
    }

    bool IsCheckedInput()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("Keyboard");
            return true;
        }
        
        if (trigger.GetState(SteamVR_Input_Sources.Any))
            return true;

        if (grab.GetState(SteamVR_Input_Sources.Any))
            return true;

        return false;
    }
}

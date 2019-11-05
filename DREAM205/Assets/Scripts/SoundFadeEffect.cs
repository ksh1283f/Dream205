using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum E_EffectType
{
    None,
    IncreaseFromZeroToMiddle,   // 0 -> 0.5
    IncreaseFromMiddleToOne,    // 0.5 -> 1
    IncreaseFromZeroToOne,  // 0 -> 1

    DecreaseFromOneToMiddle,    // 1 -> 0.5
    DecreaseFromMiddleToZero,    // 0.5 -> 1
    DecreaseFromOneToZero,  // 1 -> 0

    IncreaseFromStartCustomValToEndCustomVal,
    DecreaseFromStartCustomValToEndCustomVal,
}

public class SoundFadeEffect : MonoBehaviour
{
    AudioSource source;
    [SerializeField] float fadeDuration;
    [SerializeField] E_EffectType EffectType;
    [SerializeField] bool isPlayOnStart;

    [SerializeField] [Range(0, 1)] float startCustomVal;
    [SerializeField] [Range(0, 1)] float endCustomVal;

    public float StartCustomVal { get { return startCustomVal; } }
    public float EndCustomVal { get { return endCustomVal; } }
    public float FadeDuration { get { return fadeDuration; } }

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    private void Start()
    {
        if (isPlayOnStart)
            StartCoroutine(FadeEffect());
    }

    public IEnumerator FadeEffect(float delay = 0)
    {
        if (source == null)
        {
            Debug.LogError("ringSound is null");
            yield break;
        }

        float startTime = 0;
        float volume = source.volume;
        float startVolumeVal = -1f;
        float endVolumeVal = -1f;

        switch (EffectType)
        {
            case E_EffectType.IncreaseFromZeroToOne:
                startVolumeVal = 0f;
                endVolumeVal = 1f;
                break;

            case E_EffectType.IncreaseFromMiddleToOne:
                startVolumeVal = 0.5f;
                endVolumeVal = 1f;
                break;

            case E_EffectType.IncreaseFromZeroToMiddle:
                startVolumeVal = 0f;
                endVolumeVal = 0.5f;
                break;

            case E_EffectType.DecreaseFromOneToMiddle:
                startVolumeVal = 1f;
                endVolumeVal = 0.5f;
                break;

            case E_EffectType.DecreaseFromMiddleToZero:
                startVolumeVal = 0.5f;
                endVolumeVal = 0f;
                break;

            case E_EffectType.DecreaseFromOneToZero:
                startVolumeVal = 1f;
                endVolumeVal = 0f;
                break;

            case E_EffectType.IncreaseFromStartCustomValToEndCustomVal:
            case E_EffectType.DecreaseFromStartCustomValToEndCustomVal:
                startVolumeVal = startCustomVal;
                endVolumeVal = endCustomVal;
                break;
        }

        if (startVolumeVal == -1 || endVolumeVal == -1)
        {
            Debug.LogError("Sound effect type is invalid : " + EffectType);
            yield break;
        }

        yield return new WaitForSeconds(delay);

        if (!source.isPlaying)  // 재생중일때는 다시 재생하지 않도록
            source.Play();

        volume = Mathf.Lerp(startVolumeVal, endVolumeVal, startTime);
        while (isCompleteEffect(EffectType, volume, endVolumeVal))
        {
            startTime += Time.deltaTime / FadeDuration;
            volume = Mathf.Lerp(startVolumeVal, endVolumeVal, startTime);
            source.volume = volume;

            yield return null;
        }
    }

    public void SetFadeDuration(float value)
    {
        FadeDuration = value;
    }

    public void SetEffectType(E_EffectType type)
    {
        EffectType = type;
    }

    public void SetEffectVolumes(float start, float end)
    {
        if (start > 1 || start < 0 || end > 1 || end < 0)
        {
            Debug.LogError("invalid volume value ");
            return;
        }

        startCustomVal = start;
        endCustomVal = end;
    }

    public void StopAmbience()
    {
        if (source == null)
            return;

        source.Stop();
    }

    bool isCompleteEffect(E_EffectType type, float volume, float destVal)
    {
        switch (type)
        {
            case E_EffectType.IncreaseFromZeroToOne:
            case E_EffectType.IncreaseFromMiddleToOne:
            case E_EffectType.IncreaseFromZeroToMiddle:
            case E_EffectType.IncreaseFromStartCustomValToEndCustomVal:
                return volume < destVal;

            case E_EffectType.DecreaseFromOneToMiddle:
            case E_EffectType.DecreaseFromMiddleToZero:
            case E_EffectType.DecreaseFromOneToZero:
            case E_EffectType.DecreaseFromStartCustomValToEndCustomVal:
                return volume > destVal;
        }

        Debug.LogError("effectType error");
        return false;
    }
}

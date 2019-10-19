﻿using System;
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
}

public class SoundFadeEffect : MonoBehaviour
{
    AudioSource source;
    [SerializeField] float FadeDuration;
    [SerializeField] E_EffectType EffectType;
    [SerializeField] bool isPlayOnStart;

    private void Awake()
    { 
        source = GetComponent<AudioSource>();
    }

    private void Start()
    {
        if (isPlayOnStart)
            StartCoroutine(FadeEffect());
    }

    public IEnumerator FadeEffect(float delay =0)
    {
        if (source == null)
        {
            Debug.LogError("ringSound is null");
            yield break;
        }

        float startTime = 0;
        float volume = source.volume;
        float startVolumeVal=-1f;
        float endVolumeVal=-1f;

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

            
        }

        if(startVolumeVal == -1 || endVolumeVal == -1)
        {
            Debug.LogError("Sound effect type is invalid : "+EffectType);
            yield break;
        }

        yield return new WaitForSeconds(delay);
        
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
                return volume < destVal;

            case E_EffectType.DecreaseFromOneToMiddle:
            case E_EffectType.DecreaseFromMiddleToZero:
            case E_EffectType.DecreaseFromOneToZero:
                return volume > destVal;
        }

        Debug.LogError("effectType error");
        return false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum E_EffectType
{
    None,
    IncreaseToEnd,
    IncreaseToMiddle,
    DecreaseToMiddle,
    DecreaseToEnd,
}

public class SoundFadeEffect : MonoBehaviour
{
    AudioSource source;
    [SerializeField] float FadeDuration;
    [SerializeField] E_EffectType EffectType;

    private void Awake()
    { 
        source = GetComponent<AudioSource>();
    }

    private void Start()
    {
        StartCoroutine(FadeEffect());
    }

    public IEnumerator FadeEffect()
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
            case E_EffectType.IncreaseToEnd:
                startVolumeVal = 0f;
                endVolumeVal = 1f;
                break;

            case E_EffectType.IncreaseToMiddle:
                startVolumeVal = 0f;
                endVolumeVal = 0.5f;
                break;

            case E_EffectType.DecreaseToMiddle:
                startVolumeVal = 1f;
                endVolumeVal = 0.5f;
                break;

            case E_EffectType.DecreaseToEnd:
                startVolumeVal = 1f;
                endVolumeVal = 0f;
                break;
        }

        if(startVolumeVal == -1 || endVolumeVal == -1)
        {
            Debug.LogError("Sound effect type is invalid : "+EffectType);
            yield break;
        }
        
        volume = Mathf.Lerp(startVolumeVal, endVolumeVal, startTime);
        while (volume > 0f)
        {
            startTime += Time.deltaTime / FadeDuration;
            volume = Mathf.Lerp(startVolumeVal, endVolumeVal, startTime);
            source.volume = volume;

            yield return null;
        }
    }
}

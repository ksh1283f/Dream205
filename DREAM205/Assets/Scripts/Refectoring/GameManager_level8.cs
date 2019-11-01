using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager_level8 : MonoBehaviour
{
    [SerializeField] Animation flickAni;
    [SerializeField] Image blackImage;
    [SerializeField] SoundFadeEffect ambience;
    [SerializeField] int ambienceStopIndex;

    void Start()
    {
        if (blackImage != null)
            blackImage.gameObject.SetActive(false);

        SoundManager.Instance.OnSoundStart += OnDirectAmbience;

        StartCoroutine(SoundManager.Instance.IntroSoundPlay());
        SoundManager.Instance.OnSoundPlayEnd += LoadNextScene;
        SoundManager.Instance.OnSoundPlayEnd += () =>
        {
            if (blackImage != null)
                blackImage.gameObject.SetActive(true);
            if (ambience != null)
                ambience.StopAmbience();
        };
    }

    // 씬로딩
    void LoadNextScene()
    {
        StartCoroutine(loadNextScene());
    }

    IEnumerator loadNextScene()
    {
        if (flickAni != null)
        {
            flickAni.Play();
            while (flickAni.isPlaying)
                yield return null;
        }

        // 엔딩 크레딧 씬로딩
        SceneLoadingManager.Instance.StartSceneLoadingWithDelay(E_SceneType.level9Ending, 3);
    }

    void OnDirectAmbience(int index)
    {
        if (index != ambienceStopIndex )
            return;

        if (ambience == null)
        {
            Debug.LogError("ambience is null");
            return;
        }

        if (index == ambienceStopIndex)
        {
            ambience.StopAmbience();
            ambience.SetEffectType(E_EffectType.IncreaseFromZeroToOne);
            ambience.SetFadeDuration(3f);
            StartCoroutine(ambience.FadeEffect(3f));
        }
    }
}

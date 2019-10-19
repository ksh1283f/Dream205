using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager_Bathroom7 : MonoBehaviour
{
    [SerializeField] Animation flickAni;
    [SerializeField] SoundFadeEffect ambience;

    void Start()
    {
        StartCoroutine(SoundManager.Instance.IntroSoundPlay());
        SoundManager.Instance.OnSoundPlayEnd += LoadNextScene;
    }

    // 씬로딩
    void LoadNextScene()
    {
        ambience.SetEffectType(E_EffectType.DecreaseFromOneToZero);
        ambience.SetFadeDuration(3f);
        StartCoroutine(ambience.FadeEffect());
        StartCoroutine(loadNextScene());
    }

    IEnumerator loadNextScene()
    {
        if (flickAni == null)
        {
            Debug.LogError("flickAni is null");
            yield break;
        }

        flickAni.Play();
        while (flickAni.isPlaying)
            yield return null;

        SceneLoadingManager.Instance.StartSceneLoadingWithDelay(E_SceneType.level8Elevator, 1);
    }
}
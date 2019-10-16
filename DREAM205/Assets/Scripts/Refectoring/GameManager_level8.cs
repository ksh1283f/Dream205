using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager_level8 : MonoBehaviour
{
    [SerializeField] Animation flickAni;

    void Start()
    {
        StartCoroutine(SoundManager.Instance.IntroSoundPlay());
        SoundManager.Instance.OnSoundPlayEnd += LoadNextScene;
    }

    // 씬로딩
    void LoadNextScene()
    {
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

        // 엔딩 크레딧 씬로딩
        SceneLoadingManager.Instance.StartSceneLoadingWithDelay(E_SceneType.level6Bathroom, 0);
    }

    void PlayNoise(int index)
    {
        if(index == 1)  // soundManager의 DataList의 인덱스 첫번째에서 재생..
        {
            // todo '완료' 대사부터
        }
    }
}

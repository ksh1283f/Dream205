using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager_Room3 : MonoBehaviour
{
    [SerializeField]Animation flickAni;

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
        if(flickAni == null)
        {
            Debug.LogError("flickAni is null");
            yield break;
        }

        while (flickAni.isPlaying)
            yield return null;
        
        SceneLoadingManager.Instance.StartSceneLoadingWithDelay(E_SceneType.level4Kitchen, 0);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager_Ending : Singletone<GameManager_Ending>
{
    private void Start()
    {
        SoundManager.Instance.OnSoundPlayEnd += LoadNextScene;
        StartCoroutine(SoundManager.Instance.IntroSoundPlay());
    }

    void LoadNextScene()
    {
        SceneLoadingManager.Instance.StartSceneLoadingWithDelay(E_SceneType.levelOpening, 0);
    }
}
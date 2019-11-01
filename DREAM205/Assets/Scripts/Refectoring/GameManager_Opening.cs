using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum E_GameStartType
{
    None,
    InputSpaceKey,
    AutoPlayWithDelay,  // 10초?
}

public class GameManager_Opening : Singletone<GameManager_Opening>
{
    [SerializeField] Animation fadeOut;
    [SerializeField] E_GameStartType startType;
    [SerializeField] float delayTime;
    // public SoundManager soundManager;

    void Start()
    {
        if (startType != E_GameStartType.AutoPlayWithDelay)
            return;

        SoundManager.Instance.OnSoundPlayEnd += () => { SceneLoadingManager.Instance.StartSceneLoadingWithDelay(E_SceneType.level0Elevator, 0); };
    }

    void Update()
    {
        if (startType != E_GameStartType.InputSpaceKey)
            return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(fadeOut == null)
            {
                Debug.LogError("fadeOut is null");
                return;
            }
            // StartCoroutine(IntroSoundPlay());
            StartCoroutine(SoundManager.Instance.IntroSoundPlay());
           // SceneLoadingManager.Instance.StartSceneLoadingWithDelay(E_SceneType.level0Elevator, 0, fadeOut);
        }
    }
}

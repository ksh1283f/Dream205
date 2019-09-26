using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum E_SceneType
{
    None,
    level0Elevator,
    level1FrontDoor,
    level2Room,
    level3Room,
}

public class SceneLoadingManager : Singletone<SceneLoadingManager>
{
    Coroutine coroutine = null;

    private E_SceneType sceneType = E_SceneType.None;
    public E_SceneType SceneType
    {
        get { return sceneType; }
        set
        {
            if (value == sceneType)
                return;

            sceneType = value;
            switch (sceneType)
            {
                case E_SceneType.level0Elevator:
                case E_SceneType.level1FrontDoor:
                case E_SceneType.level2Room:
                case E_SceneType.level3Room:
                    StartSceneLoading(sceneType);
                    break;
             
            }
        }
    }

    private void Start()
    {
        sceneType = E_SceneType.level0Elevator; // 정상적으로 플레이한다고 가정했을 때
    }

    void StartSceneLoading(E_SceneType type)
    {
        if (coroutine == null)
            coroutine = StartCoroutine(startSceneLoading(type));
    }

    IEnumerator startSceneLoading(E_SceneType type)
    {
        AsyncOperation ao = SceneManager.LoadSceneAsync(type.ToString());
        while (!ao.isDone)
            yield return null;

        // 로딩 후 할일
        coroutine = null;
    }
}

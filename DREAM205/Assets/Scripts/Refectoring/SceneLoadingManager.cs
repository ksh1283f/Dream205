using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Valve.VR;

public enum E_SceneType
{
    None,
    level0Elevator,
    level1FrontDoor,
    level2Room,
    level3Room,
    level4Kitchen,
    level5Kitchen,
    level6Bathroom,
    level7Bathroom,
    level8Elevator,
    endingScene,
}

public class SceneLoadingManager : Singletone<SceneLoadingManager>
{
    [SerializeField] SteamVR_LoadLevel vrLoadLevel;
    Coroutine coroutine = null;

    [SerializeField] private E_SceneType sceneType = E_SceneType.None;
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
                case E_SceneType.level4Kitchen:
                case E_SceneType.level5Kitchen:
                case E_SceneType.level6Bathroom:
                case E_SceneType.level7Bathroom:
                case E_SceneType.level8Elevator:

                    StartSceneLoading(sceneType);
                    break;
             
            }
        }
    }

    public IGameManagerInteract PresentGameManager
    {
        get
        {
            switch (SceneType)
            {
                case E_SceneType.None:
                    break;
                case E_SceneType.level0Elevator:
                    break;
                case E_SceneType.level1FrontDoor:
                    break;
                case E_SceneType.level2Room:
                    return GameManager_Room2.Instance;
                    
                case E_SceneType.level3Room:
                    break;
                case E_SceneType.level4Kitchen:
                    return GameManager_Kitchen4.Instance;
                    
                case E_SceneType.level5Kitchen:
                    break;

                //case E_SceneType.level6Bathroom:
                //return GameManager_Bathroom6.Instance;

                case E_SceneType.level7Bathroom:
                break;

                case E_SceneType.level8Elevator:
                break;

                default:
                    break;
            }

            return null;
        }
    }

    private void Start()
    {
       // sceneType = E_SceneType.level0Elevator; // 정상적으로 플레이한다고 가정했을 때
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

    // 딜레이 타임 이후 로딩
    public void StartSceneLoadingWithDelay(E_SceneType type, float delay, Animation fadeAni = null)
    {
        if (coroutine == null)
            coroutine = StartCoroutine(SceneLoadingWithDelay(type, delay, fadeAni));
    }

    IEnumerator SceneLoadingWithDelay(E_SceneType type, float delay, Animation fadeAni= null)
    {
        // 페이드 효과가 필요한 경우
        if(fadeAni != null)
        {
            fadeAni.Play();
            while (fadeAni.isPlaying)
                yield return null;
        }

        // 딜레이가 있는 경우
        if(delay > 0)
            yield return new WaitForSeconds(delay);

        vrLoadLevel.levelName = type.ToString();
        vrLoadLevel.Trigger();
        while (SteamVR_LoadLevel.progress < 1)
            yield return null;
        

        //AsyncOperation ao = SceneManager.LoadSceneAsync(type.ToString());

        //while (!ao.isDone)
        //    yield return null;

        // 로딩 후 할일
        sceneType = type;
        coroutine = null;
    }
}

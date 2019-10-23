using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager_Opening : Singletone<GameManager_Opening>
{
    [SerializeField] Animation fadeOut;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(fadeOut == null)
            {
                Debug.LogError("fadeOut is null");
                return;
            }

            SceneLoadingManager.Instance.StartSceneLoadingWithDelay(E_SceneType.level0Elevator, 0, fadeOut);
        }
    }
}

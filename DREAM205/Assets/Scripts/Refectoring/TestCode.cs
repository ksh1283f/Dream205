using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class TestCode : MonoBehaviour
{

    [SerializeField]SteamVR_LoadLevel loadLevel;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            loadLevel.Trigger();
            
        }
        Debug.Log(SteamVR_LoadLevel.progress);
    }
}

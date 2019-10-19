using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class TestCode : MonoBehaviour
{

    [SerializeField]SteamVR_LoadLevel loadLevel;
    SoundFadeEffect effect;

    private void Start()
    {
        effect = FindObjectOfType<SoundFadeEffect>();
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            //loadLevel.Trigger();
            effect.StopAmbience();
            
        }
        //Debug.Log(SteamVR_LoadLevel.progress);
    }
}

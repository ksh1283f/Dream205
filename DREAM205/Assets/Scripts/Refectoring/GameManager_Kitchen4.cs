﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager_Kitchen4 : Singletone<GameManager_Kitchen4>, IGameManagerInteract
{
    [SerializeField] AudioSource Sound;
    [SerializeField] float fadeDuration;
    [SerializeField] float nextSceneDelayTime;
    [SerializeField] Transform spiderTransFromMicroWave;
    [SerializeField] Transform spiderTransFromHands;
    [SerializeField] Transform spiderTransFromSpeaker;
   // [SerializeField] Transform spiderTransFromDrawing;
    [SerializeField] Animation fadeAni;
    [SerializeField] SoundFadeEffect ambience;

    public GameObject Maggot;

    private void Start()
    {
        SoundManager.Instance.OnSoundPlayEnd += LoadNextScene;
        StartCoroutine(SoundManager.Instance.IntroSoundPlay());
    }

    public void CreateMaggot(E_RoomInteractObjType type)
    {
        Vector3 pos;
        switch (type)
        {
            case E_RoomInteractObjType.Microwave:
                pos = spiderTransFromMicroWave.position;
                break;

            case E_RoomInteractObjType.Hands:
                pos = spiderTransFromHands.position;
                break;

            case E_RoomInteractObjType.Speaker:
                pos = spiderTransFromSpeaker.position;
                break;

           // case E_RoomInteractObjType.Drawing:
            //    pos = spiderTransFromDrawing.position;
            //    break;

            default:
                Debug.LogError("KitchenInteractObjType is none");
                return;
        }


        GameObject spider = Instantiate(Maggot, pos, Quaternion.identity);
        spider.transform.eulerAngles = new Vector3(-90, 0, 180);
    }

    public void RemoveSound(AudioSource audio)
    {
        Sound = audio;
        StartCoroutine(FadeSound());
    }

    IEnumerator FadeSound()
    {
        if (Sound == null)
        {
            Debug.LogError("ringSound is null");
            yield break;
        }

        float volume = Sound.volume;
        float startTime = 0;
        volume = Mathf.Lerp(1f, 0, startTime);
        while (volume > 0f)
        {
            startTime += Time.deltaTime / fadeDuration;
            volume = Mathf.Lerp(1f, 0, startTime);
            Sound.volume = volume;

            yield return null;
        }
    }

    void LoadNextScene()
    {
        ambience.SetEffectType(E_EffectType.DecreaseFromOneToZero);
        ambience.SetFadeDuration(nextSceneDelayTime);
        StartCoroutine(ambience.FadeEffect());
        SceneLoadingManager.Instance.StartSceneLoadingWithDelay(E_SceneType.level5Kitchen, nextSceneDelayTime, fadeAni);
    }
}

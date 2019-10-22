using System.Collections;
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
    [SerializeField] Animation fadeAni;
    [SerializeField] SoundFadeEffect ambience;

    [SerializeField] AudioSource announce1;
    [SerializeField] AudioSource announce2;
    [SerializeField] InteractiveProps speaker;  // 별도의 처리가 필요한 오브젝트
    [SerializeField] int speakerDirectingIndex;

    // for debug
    [SerializeField] AudioSource nowPlaying;

    public GameObject Maggot;

    private void Start()
    {
        SoundManager.Instance.OnSoundPlayEnd += LoadNextScene;
        SoundManager.Instance.OnSoundStart += ExecuteSpeakerSound;
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
        StopCoroutine(executeSpeakerSound());
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

    void ExecuteSpeakerSound(int index)
    {
        if (index != speakerDirectingIndex)
            return;

        StartCoroutine(executeSpeakerSound());
    }

    IEnumerator executeSpeakerSound()
    {
        if(announce1 == null)
        {
            Debug.LogError("announce1 is null");
            yield break;
        }

        if (announce2 == null)
        {
            Debug.LogError("announce2 is null");
            yield break;
        }

        nowPlaying = announce1;
        speaker.sound = announce1;
        announce1.Play();
        while (announce1.isPlaying)
            yield return null;

        nowPlaying = announce2;
        speaker.sound = announce2;
        announce2.Play();
        while(announce2.isPlaying)
        {
            if (speaker.IsInteractionEnd)
                speaker.sound.Stop();
            yield return null;
        }
    }
}

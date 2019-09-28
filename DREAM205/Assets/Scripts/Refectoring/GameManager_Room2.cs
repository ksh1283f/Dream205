using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager_Room2 : Singletone<GameManager_Room2>
{
    [SerializeField] AudioSource Sound;
    [SerializeField] float fadeDuration;
    [SerializeField] Transform spiderTransFromRadio;
    [SerializeField] Transform spiderTransFromCloset;
    [SerializeField] Transform spiderTransFromCusion;

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
            case E_RoomInteractObjType.Radio:
                pos = spiderTransFromRadio.position;
                break;

            case E_RoomInteractObjType.Closet:
                pos = spiderTransFromCloset.position;
                break;

            case E_RoomInteractObjType.Cusion:
                pos = spiderTransFromCusion.position;
                break;

            default:
                Debug.LogError("RoomInteractObjType is none");
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
        SceneLoadingManager.Instance.SceneType = E_SceneType.level3Room;
    }
}

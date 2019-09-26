using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorOpen : MonoBehaviour
{
    public Animation DoorAni;
    public Animation LightOff;
    public AudioSource DoorSound;
    [SerializeField] AudioSource phoneRing;
    [SerializeField] float fadeDuration;

    // Start is called before the first frame update
    void Start()
    {
       // DoorAni = GetComponent<Animation>();
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name==("Controller (left)"))
        {
            DoorAni.Play();
            DoorSound.Play();
        }
    }

    void Flicker()
    {
        LightOff.Play();
    }

    void OnDoorAniEnd()
    {
        StartCoroutine(FadeSound());
        //SceneManager.LoadScene("level2Room"); // 로딩은 코루틴 종료 후
    }

    IEnumerator FadeSound()
    {
        if (phoneRing == null)
        {
            Debug.LogError("ringSound is null");
            yield break;
        }

        float volume = phoneRing.volume;
        float startTime = 0;
        volume = Mathf.Lerp(0.173f, 0, startTime);
        while (volume > 0f)
        {
            startTime += Time.deltaTime / fadeDuration;
            volume = Mathf.Lerp(0.173f, 0, startTime);
            phoneRing.volume = volume;

            yield return null;
        }

        SceneLoadingManager.Instance.SceneType = E_SceneType.level2Room;
    }
}

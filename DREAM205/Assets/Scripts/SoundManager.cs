using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class SoundManager : MonoBehaviour
{
    public Action<int> OnSoundStart { get; set; }
    public Action<int> OnSoundEnd { get; set; }

    public Animation StartFo;
    //AudioClip narr;
    public List<AudioSource> SoundList = new List<AudioSource>();

    bool isStart = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!isStart)
            {
                isStart = true;
                StartCoroutine(SoundPlay());
            }
        }

    }

    public IEnumerator SoundPlay()
    {
        StartFo.Play();
        while (StartFo.isPlaying)
            yield return null;

        yield return new WaitForSeconds(4f);

        for (int i = 0; i < SoundList.Count; i++)
        {
            SoundList[i].Play();
            if (OnSoundStart != null)
                OnSoundStart(i);

            while (SoundList[i].isPlaying)
                yield return null;

            if (OnSoundEnd != null)
                OnSoundEnd(i);
        }

    }

    // Update()
    // {

    // }

}

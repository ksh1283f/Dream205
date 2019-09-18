using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class SoundManager : MonoBehaviour
{
    //public Animation anim;
    //AudioClip narr;
    public List<AudioSource> SoundList = new List<AudioSource>();

    //  private void Awake()
    // {

    //  }
    public void StartPlay()
    {
        StartCoroutine(PlayerSound());
    }

    IEnumerator PlayerSound()
    {
        // anim.Play();
        //    while (anim.isPlaying)
        //   yield return null;

        yield return new WaitForSeconds(4f);
        for (int i = 0; i < SoundList.Count; i++)
        {
            SoundList[i].Play();
            while (SoundList[i].isPlaying)
                yield return null;
        }

    }


   // Update()
   // {
        
   // }

}

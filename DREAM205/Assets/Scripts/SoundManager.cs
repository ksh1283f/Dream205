using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class SoundManager : MonoBehaviour
{
    public Animation StartFo;
    //AudioClip narr;
    public List<AudioSource> SoundList = new List<AudioSource>();

    //  private void Awake()
    // {

    //  }


    IEnumerator Start()
    {
        if(StartFo.Play());
        {
            while (StartFo.isPlaying)
                yield return null;
        }

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

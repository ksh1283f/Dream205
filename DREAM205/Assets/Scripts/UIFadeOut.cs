using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFadeOut : MonoBehaviour
{
   public Animation StartFo;
   // public Animation AmbiFI;
    public AudioSource Ambience;
  //  bool isFadeOutComplete = false;
   // public SoundManager soundManager;



    void Start()
    {
        StartFo = GetComponent<Animation>();
    }

 
    void Update()
    {
         if (Input.GetKeyDown(KeyCode.Space))
          {
            StartFo.Play();
            Ambience.Play();
          }

       /* if (StartFo.isPlaying)
        {
            Ambience.Play();
            //AmbiFI.Play();
        }*/
        
    }
}

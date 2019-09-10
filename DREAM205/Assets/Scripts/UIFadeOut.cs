using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFadeOut : MonoBehaviour
{
   public Animation StartFo;


    void Start()
    {
        StartFo = GetComponent<Animation>();
    }

 
    void Update()
    {
         if (Input.GetKeyDown("0"))
          {
            StartFo.Play();
          }
    }
   
}

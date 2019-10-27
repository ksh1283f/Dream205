using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlicker : MonoBehaviour
{
    AudioSource FlickerSound;

    // Start is called before the first frame update
    void Start()
    {
        FlickerSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
  
    void Playsound()
    {
        FlickerSound.Play();
    }
}

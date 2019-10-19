using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneRing : MonoBehaviour
{
    AudioSource m_sound;
    public AudioSource RingSound1;
    public AudioSource RingSound2;
    // Start is called before the first frame update
    void Start()
    {
        m_sound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_sound.isPlaying)
        {
            RingSound1.Play();
            RingSound2.Play();
        }
    }
}

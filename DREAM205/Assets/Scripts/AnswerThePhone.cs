using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerThePhone : InteractableObj
{
    AudioSource m_audioSource;
    // Start is called before the first frame update
    void Start()
    {
        m_audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y > 1f)
        {
            m_audioSource.Play();
            isInteractionEnd = true;
        }
       // if (m_audioSource.time>0)
       // {
       //     Destroy(m_audioSource);
      //  }
    }
}

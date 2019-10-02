using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerThePhone : InteractableObj
{
    //[SerializeField] float interactPosValue = 1f; //1f말고 다른값을 기준으로 할때 사용
    AudioSource m_audioSource;
    public AudioSource phoneRing;
    // Start is called before the first frame update
    void Start()
    {
        m_audioSource = GetComponent<AudioSource>();
        Invoke("StartPhoneRing", 2f);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y > 1f && !isInteractionEnd)
        {
            if(phoneRing != null)
                phoneRing.Stop();

            if(m_audioSource != null)
                m_audioSource.Play();
            isInteractionEnd = true;
        }
       // if (m_audioSource.time>0)
       // {
       //     Destroy(m_audioSource);
      //  }
    }

    void StartPhoneRing()
    {
        if (phoneRing == null)
            return;

        phoneRing.Play();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextFo : MonoBehaviour
{
    private Animation TextFadeOut;
   
    void Start()
    {
        TextFadeOut = GetComponent<Animation>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("1"))
        {
            TextFadeOut.Play();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public Animation anim;

    // Start is called before the first frame update
   // IEnumerator Start()
  //  {

   // }

    // Update is called once per frame
    IEnumerator Update()
    {
        if (anim.isPlaying)
        {
            yield return new WaitForSeconds(3.0f);
        }
    }

}

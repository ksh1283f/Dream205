using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorObject : MonoBehaviour
{
    private Animation DoorAni;
   
    // Start is called before the first frame update
    void Start()
    {
        DoorAni = GetComponent<Animation>();
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("handle"))
        {
            DoorAni.Play();
        }    
    }

    void SceneLoading()
    {

            SceneManager.LoadScene(1);

    }
    
    // Update is called once per frame
    void Update()
    {
      /*  {
            if (DoorAni.isPlaying)
            {
                SceneManager.LoadScene(1);
            }
        }*/
    }
}

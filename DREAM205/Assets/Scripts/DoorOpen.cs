using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorOpen : MonoBehaviour
{
    public Animation DoorAni;
    public Animation LightOff;
    public AudioSource DoorSound;

    // Start is called before the first frame update
    void Start()
    {
       // DoorAni = GetComponent<Animation>();
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name==("Controller (left)"))
        {
            DoorAni.Play();
            DoorSound.Play();
        }
    }

    void Flicker()
    {
        LightOff.Play();
    }

    void SceneLoading()
    {

        SceneManager.LoadScene("level2Room");

    }
}

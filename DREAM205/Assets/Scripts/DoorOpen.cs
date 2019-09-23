using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorOpen : MonoBehaviour
{
    private Animation DoorAni;

    // Start is called before the first frame update
    void Start()
    {
        DoorAni = GetComponent<Animation>();
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name==("Controller (left)"))
        {
            DoorAni.Play();
        }
    }

    void SceneLoading()
    {

        SceneManager.LoadScene("level2Room");

    }
}

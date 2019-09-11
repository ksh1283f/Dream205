using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveProps : MonoBehaviour
{
    //  private GameObject Cubes;
    // public GameObject Sphere;
    // private AudioSource Sound;
    public GameManager gameManager;
    public AudioSource sound;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Interactables")
        {
            // var Sound = GameObject.FindWithTag("Sound");
            // Instantiate(Sphere, Cubes.transform.position, Quaternion.identity);
            // Destroy(Sound);
            gameManager.CreateMaggot(transform.position);
            gameManager.RemoveSound(sound);
            gameManager.m_RemoveObj(gameObject);
        }

    }
}

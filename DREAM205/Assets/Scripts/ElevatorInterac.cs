using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorInterac : MonoBehaviour
{
    public GameManager gameManager;
   // public MeshRenderer m_mesh;
   // public AudioSource sound;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "FloorNumber")
        {
            // var Sound = GameObject.FindWithTag("Sound");
            // Instantiate(Sphere, Cubes.transform.position, Quaternion.identity);
            // Destroy(Sound);

            //gameManager.CreateMaggot(transform.position);
            // gameManager.RemoveSound(sound);
            // gameManager.m_RemoveObj(gameObject);

            //gameManager.EmissionOn(m_mesh);
            other.GetComponent<Renderer>().material.SetColor("_EmissionColor", new Color(255, 0, 0));
        }

    }

}

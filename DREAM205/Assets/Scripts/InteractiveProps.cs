using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BeautifulDissolves;


public class InteractiveProps : MonoBehaviour
{
    //  private GameObject Cubes;
    // public GameObject Sphere;
    // private AudioSource Sound;
    public GameManager gameManager;
    public AudioSource sound;
    public Dissolve m_dissolve;
    DissolveSettings m_DissolveSettings;
    public Material m_material;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Interactables")
        {
            // var Sound = GameObject.FindWithTag("Sound");
            // Instantiate(Sphere, Cubes.transform.position, Quaternion.identity);
            // Destroy(Sound);
            gameManager.CreateMaggot(transform.position);
            gameManager.RemoveSound(sound);
            //gameManager.m_RemoveObj();
            GetComponent<MeshRenderer>().material = m_material;
            m_dissolve.TriggerDissolve(m_material);

        }
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}

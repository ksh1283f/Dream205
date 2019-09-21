using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ElevatorInterac : MonoBehaviour
{
    public GameManager gameManager;
    AudioSource LaserFx;
    public AudioSource plant;
    public AudioSource ElevatorAmbience;
    
   // public SoundManager soundManager;

    //public GameObject secondFloor;
   // public GameObject fifthFloor;
   // public GameObject fourteen;

    // public MeshRenderer m_mesh;
    // public AudioSource sound;
    private List<Renderer> rendererList = new List<Renderer>();
    private void Start()
    {
        LaserFx = GetComponent<AudioSource>();
        rendererList.Add(GetComponent<Renderer>());
        if (rendererList[0] == null)
            rendererList = GetComponentsInChildren<Renderer>().ToList();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Interactables")
        {
            Debug.LogError("FloorNumber");
            LaserFx.Play();
            // var Sound = GameObject.FindWithTag("Sound");
            // Instantiate(Sphere, Cubes.transform.position, Quaternion.identity);
            // Destroy(Sound);

            //gameManager.CreateMaggot(transform.position);
            // gameManager.RemoveSound(sound);
            // gameManager.m_RemoveObj(gameObject);

            //gameManager.EmissionOn(m_mesh);
            //GetComponent<Renderer>().material.SetColor("_EmissionColor", new Color(255, 0, 0));
            for (int i = 0; i < rendererList.Count; i++)
                rendererList[i].material.SetColor("_EmissionColor", new Color(255, 0, 0));

        }
        if (other.gameObject.name=="close")
        {
            plant.Play();
        }

        if (other.gameObject.name == "no2")
        {
            
        }
    }

   /* private void Update()
    {
        if (true)
        {
            secondFloor.gameObject.GetComponent<BoxCollider>().enabled = false;
        }

    }*/

}

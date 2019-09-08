using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bug : MonoBehaviour
{
    private object Cubes;

    public GameObject Sphere;
    // public AudioSource Sound;

    // Start is called before the first frame update
    void Start()
    {
      //  var Cube = GameObject.FindWithTag("Interactables");
      //  var Sound = GameObject.FindWithTag("Sound");
    }

    // Update is called once per frame
    void Update()
    {
        if (Cubes==null)
        {
            var Cubes = GameObject.FindWithTag("Interactables");
            var Sound = GameObject.FindWithTag("Sound");
            Instantiate(Sphere, Cubes.transform.position, Quaternion.identity);
            Destroy(Sound);
        }
    
    }
}

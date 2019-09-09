using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private GameObject Cubes;
    private AudioSource Sound;
    public GameObject Sphere;

    public void CreateSphere(Vector3 pos)
    {
        Instantiate(Sphere, pos, Quaternion.identity);
    }

    public void RemoveSound(AudioSource audio)
    {
        Sound = audio;
        Sound.Stop();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

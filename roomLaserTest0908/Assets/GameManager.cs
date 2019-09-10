using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //public GameObject CubeLiquid;
    //public GameObject CubeWalking;

    private AudioSource Sound;
    public GameObject Sphere;

    public void RemoveCube(GameObject obj)
    {
        obj.SetActive(false);
    }

    public void CreateSphere(Vector3 pos)
    {
        Instantiate(Sphere, pos, Quaternion.identity);
    }

    public void RemoveSound(AudioSource audio)
    {
        Sound = audio;
        Sound.Stop();
    }
}

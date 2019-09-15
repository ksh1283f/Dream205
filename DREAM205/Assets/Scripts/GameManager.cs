using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //public GameObject CubeLiquid;
    //public GameObject CubeWalking;

    private AudioSource Sound;
    public GameObject Maggot;

    public void m_RemoveObj(GameObject obj)
    {
        obj.SetActive(false);
    }

    public void CreateMaggot(Vector3 pos)
    {
        Instantiate(Maggot, pos, Quaternion.identity);
    }

    public void RemoveSound(AudioSource audio)
    {
        Sound = audio;
        Sound.Stop();
    }

   // public void EmissionOn(MeshRenderer number)
   // {
   //     number.GetComponent<Renderer>().material.SetColor("_EmissionColor", new Color(255, 0, 0));
   // }
}

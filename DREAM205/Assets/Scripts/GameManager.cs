using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //public GameObject CubeLiquid;
    //public GameObject CubeWalking;

    private AudioSource Sound;
    public GameObject Maggot;

    public SoundManager soundManager;

    public GameObject secondFloor;
    public GameObject fifthFloor;
    public GameObject fourteen;
    public GameObject closed;


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

    private void Start()
    {
        soundManager.OnSoundPlayEnd += ButtonOrder;
        closed.gameObject.GetComponent<BoxCollider>().enabled = false;
        secondFloor.gameObject.GetComponent<BoxCollider>().enabled = false;
        fifthFloor.gameObject.GetComponent<BoxCollider>().enabled = false;
        fourteen.gameObject.GetComponent<BoxCollider>().enabled = false;

    }

    private void ButtonOrder()
    {  
            closed.gameObject.GetComponent<BoxCollider>().enabled = true;
    }

    private void Update()
    {
        if (true)//audioSource.time>0//"~원하는 층수를 눌러주세요" 재생 종료 체크
        {
            secondFloor.gameObject.GetComponent<BoxCollider>().enabled = true;
            fifthFloor.gameObject.GetComponent<BoxCollider>().enabled = true;
            fourteen.gameObject.GetComponent<BoxCollider>().enabled = true;
        }
    }
    

    // public void EmissionOn(MeshRenderer number)
    // {
    //     number.GetComponent<Renderer>().material.SetColor("_EmissionColor", new Color(255, 0, 0));
    // }
}

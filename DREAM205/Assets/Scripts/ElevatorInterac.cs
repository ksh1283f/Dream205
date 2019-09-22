using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;



public enum E_ElevatorBtnType
{
    None,
    Close,
    Second,
    Fifth,
    Fourteenth,
}

public class ElevatorInterac : InteractableObj
{
    /*새로 추가된 코드*/
    public E_ElevatorBtnType BtnType;
    public Action<E_ElevatorBtnType> OnClickedBtn { get; set; }
    /*-------------------*/


    public GameManager gameManager;

    public AudioSource plant;
    public AudioSource ElevatorAmbience;
    AudioSource LaserFx;

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
            if (OnClickedBtn != null)
                OnClickedBtn(BtnType);
        }
    }

    public void ActivateColor()
    {
        for (int i = 0; i < rendererList.Count; i++)
            rendererList[i].material.SetColor("_EmissionColor", new Color(255, 0, 0));

        isInteractionEnd = true;

    }


}

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

    public InteractableObj[] connectedInteractableObjs;
    bool isContainedConnectedObj { get { return connectedInteractableObjs != null && connectedInteractableObjs.Length > 0; } }
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
    private void Awake()
    {
        LaserFx = GetComponent<AudioSource>();
        rendererList.Add(GetComponent<Renderer>());
        if (rendererList[0] == null)
            rendererList = GetComponentsInChildren<Renderer>().ToList();

        if (isContainedConnectedObj)
        {
            for (int i = 0; i < connectedInteractableObjs.Length; i++)
            {
                if (connectedInteractableObjs[i] == null)
                {
                    Debug.LogError("connectedInteractableObjs is null, index: " + i);
                    continue;
                }
                connectedInteractableObjs[i].OnExecutedConnectedObjAction += OnExecutedObjAction;
            }
        }
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

        if (!IsInteractionEnd)
            isInteractionEnd = true;

        // 연관된 인터렉션 오브젝트가 있는경우 연관된 오브젝트들도 같이 완료되었다고 알려주기
        if (isContainedConnectedObj && OnExecutedConnectedObjAction != null)
            OnExecutedConnectedObjAction();
    }

    private void OnExecutedObjAction()
    {
        if (!IsInteractionEnd)
            isInteractionEnd = true;
    }
}

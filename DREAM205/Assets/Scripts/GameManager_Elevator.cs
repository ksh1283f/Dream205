using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager_Elevator : MonoBehaviour
{
    public GameObject secondFloor;
    public GameObject fifthFloor;
    public GameObject fourteen;
    public GameObject closed;

    private void Awake()
    {
        closed.GetComponent<InteractableObj>().OnExecuteInteract += CloseButtonOrder;
        secondFloor.gameObject.GetComponent<InteractableObj>().OnExecuteInteract += FloorButtonOrder;
        fifthFloor.gameObject.GetComponent<InteractableObj>().OnExecuteInteract = FloorButtonOrder;
        fourteen.gameObject.GetComponent<InteractableObj>().OnExecuteInteract = FloorButtonOrder;

        closed.gameObject.GetComponent<BoxCollider>().enabled = false;
        secondFloor.gameObject.GetComponent<BoxCollider>().enabled = false;
        fifthFloor.gameObject.GetComponent<BoxCollider>().enabled = false;
        fourteen.gameObject.GetComponent<BoxCollider>().enabled = false;
    }

    private void CloseButtonOrder()
    {
        closed.gameObject.GetComponent<BoxCollider>().enabled = true;
    }

    private void FloorButtonOrder()
    {
        secondFloor.gameObject.GetComponent<BoxCollider>().enabled = true;
        fifthFloor.gameObject.GetComponent<BoxCollider>().enabled = true;
        fourteen.gameObject.GetComponent<BoxCollider>().enabled = true;
    }
 
}

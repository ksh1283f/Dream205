using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObj : MonoBehaviour
{
    protected bool isInteractionEnd;
    public bool IsInteractionEnd { get { return isInteractionEnd; } }
    public Action OnExecuteInteract { get; set; }
    
    protected virtual void Start()
    {
        
    }

}

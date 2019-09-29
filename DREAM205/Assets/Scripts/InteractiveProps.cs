using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BeautifulDissolves;

public enum E_RoomInteractObjType
{
    None,
    Radio,
    Closet,
    Cusion,
    Drawing,
}

public class InteractiveProps : InteractableObj
{
    //  private GameObject Cubes;
    // public GameObject Sphere;
    // private AudioSource Sound;
    public AudioSource sound;
    public Dissolve m_dissolve;
    public Material DissolveMaterial;
    public Material GlowMaterial;
    [SerializeField] E_RoomInteractObjType objType;
    [SerializeField] Renderer propsRenderer;

    private void Start()
    {
        if (objType != E_RoomInteractObjType.Cusion)
            propsRenderer = GetComponent<Renderer>();

        OnExecuteInteract += ActivateGlowMaterial;
        GetComponent<Collider>().enabled = false;
    }   

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Interactables")
        {
            Laser laser = other.gameObject.GetComponent<Laser>();
            if (laser.isInteracted == true)
                return;

            laser.isInteracted = true;
            GameManager_Room2.Instance.CreateMaggot(objType);
            GameManager_Room2.Instance.RemoveSound(sound);

            if (objType != E_RoomInteractObjType.Cusion)
                propsRenderer.material = DissolveMaterial;
            else if (objType == E_RoomInteractObjType.Cusion && propsRenderer != null)
                propsRenderer.material = DissolveMaterial;
            else
                return;

            m_dissolve.TriggerDissolve(DissolveMaterial);
        }
    }

    public void DestroySelf()
    {
        isInteractionEnd =true;

        Destroy(gameObject);
        //gameObject.SetActive(false);
    }

    /// <summary>
    /// 메터리얼을 글로우 메터리얼로 교체: 인터랙트가 가능한 상태
    /// </summary>
    public void ActivateGlowMaterial()
    {
        if(GlowMaterial == null)
        {
            Debug.LogError(string.Concat(gameObject.name, ": glowMaterial is null"));
            return;
        }

        propsRenderer.material = GlowMaterial;
        GetComponent<Collider>().enabled = true;
    }
}

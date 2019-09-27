using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BeautifulDissolves;

    public class GameManager : MonoBehaviour
    {

        
        //public GameObject CubeLiquid;
        //public GameObject CubeWalking;

        [SerializeField] AudioSource Sound;
        [SerializeField] float fadeDuration;
        public GameObject Maggot;
   // public Dissolve m_dissolve;


    //public SoundManager soundManager;

    /*public GameObject secondFloor;
    public GameObject fifthFloor;
    public GameObject fourteen;
    public GameObject closed;*/


        //public void m_RemoveObj()
   // {
        // obj.SetActive(false);
  //      m_dissolve.TriggerDissolve();
  //  }
    

        public void CreateMaggot(Vector3 pos)
        {
            Instantiate(Maggot, pos, Quaternion.identity);
        }

        public void RemoveSound(AudioSource audio)
        {
            Sound = audio;
            // Sound.Stop();
            StartCoroutine(FadeSound());
        }

        IEnumerator FadeSound()
        {
            if (Sound == null)
            {
                Debug.LogError("ringSound is null");
                yield break;
            }

            float volume = Sound.volume;
            float startTime = 0;
            volume = Mathf.Lerp(1f, 0, startTime);
            while (volume > 0f)
            {
                startTime += Time.deltaTime / fadeDuration;
                volume = Mathf.Lerp(1f, 0, startTime);
                Sound.volume = volume;

                yield return null;
            }
        }

        /*  private void Awake()
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
      }*/

    }


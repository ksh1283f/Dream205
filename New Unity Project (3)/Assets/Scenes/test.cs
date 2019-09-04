using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class test : MonoBehaviour
{
    private AudioSource audioSource;
    // private int levelToLoad;

    
    // Start is called before the first frame update
  void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    
    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown("0"))
        {
            audioSource.Play();
                }

        if (audioSource.isPlaying)
        {
            return;
        }

  else if (audioSource.time>0)
        {
            SceneManager.LoadScene(0);
        }

    }
    public void Stop()
    {
        audioSource.Stop();
    }
    public void Delete()
    {
        Stop();
        if (!audioSource.isPlaying)
        {
            Destroy(this.gameObject);
        }
    }

/*public void NextLevel()
    {
      //levelToLoad = levelIndex;
        if (!audioSource.isPlaying)
        {
       
        
            SceneManager.LoadScene(0);
        }
    }*/

}

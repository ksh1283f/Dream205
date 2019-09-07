using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTest : MonoBehaviour
{
    private AudioSource audioSource;
    private GameObject plant;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        plant=GameObject.Find("Potted_Plant");
    }

    // Update is called once per frame
    void Update()
    {
        if (plant.transform.localPosition.y>1)
        {
            audioSource.Play();
        }

        if (audioSource.time>0)
        {
            Destroy(this.gameObject);
        }
    }
}

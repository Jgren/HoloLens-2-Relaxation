using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleMute : MonoBehaviour
{
    public GameObject gameObject;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.GetComponent<AudioSource>() != null)
        {
            AudioSource audioSource = gameObject.GetComponent<AudioSource>();
            if (audioSource.volume == 0)
            {
                audioSource.volume = 1;
            }
            else
                audioSource.volume = 0;
        }
        this.enabled = false;
    }
}

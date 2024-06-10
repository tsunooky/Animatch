using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicBihaviour : MonoBehaviour
{
    public AudioSource audioSource;
    
    void Start()
    {
        if (audioSource != null)
        {
            audioSource.Play();
        }
    }
    
    void Awake()
    {
        DontDestroyOnLoad(gameObject); 
    }
}

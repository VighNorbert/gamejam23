using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WritingSound : MonoBehaviour
{
    public AudioClip writing;
    public AudioSource source;
    
    public static WritingSound instance;
    
    private void Awake()
    {
        instance = this;
    }
    
    public void playWriting()
    {
        source.clip = writing;
        source.Play();
    }
}

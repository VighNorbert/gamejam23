using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    //click, ding, donation, drink, incorrect, writing, watering
    public AudioClip click;

    public AudioClip ding;

    public AudioClip donation;

    public AudioClip drink;

    public AudioClip incorrect;

    public AudioClip watering;
    public AudioSource source;
    
    public static SFXManager instance;
    // Start is called before the first frame update

    private void Awake()
    {
        instance = this;
    }

    public void playClick()
    {
        source.clip = click;
        source.volume = 1.0f;
        source.Play();
    }
    
    public void playDing()
    {
        source.clip = ding;
        source.volume = 1.0f;
        source.Play();
    }
    
    public void playDonation()
    {
        source.clip = donation;
        source.volume = 0.25f;
        source.Play();
    }
    
    public void playDrink()
    {
        source.clip = drink;
        source.volume = 1.0f;
        source.Play();
    }
    
    public void playIncorrect()
    {
        source.clip = incorrect;
        source.volume = 1.0f;
        source.Play();
    }
    
    public void playWatering()
    {
        source.clip = watering;
        source.volume = 1.0f;
        source.Play();
    }
}

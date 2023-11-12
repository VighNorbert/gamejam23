using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // Start is called before the first frame update

    public List<AudioClip> songs = new List<AudioClip>();
    public AudioSource source;

    public static SoundManager instance;

    public int songNumber = 0;
    public int prevSongNumber = 0;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        source.Play();
        source.volume = 0.3f;
    }

    public void NextSong()
    {
        int rand = Random.Range(0, songs.Count);
        while (rand == songNumber || rand == prevSongNumber)
        {
            rand = Random.Range(0, songs.Count);
        }
        prevSongNumber = songNumber;
        songNumber = rand;
        source.clip = songs[songNumber];
        source.Play();
    }

    public void ResetSong()
    {
        source.Stop();
        source.time = 0f;
        source.Play();
    }

    public void PreviousSong()
    {
        int help = 0;
        source.clip = songs[prevSongNumber];
        help = songNumber;
        songNumber = prevSongNumber;
        prevSongNumber = help;
        source.Play();
    }

    public void LowerVolume()
    {
        if (source.volume > 0)
        {
            source.volume -= 0.1f;
        }
    }

    public void HigherVolume()
    {
        if (source.volume < 1)
        {
            source.volume += 0.1f;
        }
    }
}

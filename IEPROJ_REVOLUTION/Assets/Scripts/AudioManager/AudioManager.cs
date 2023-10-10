using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public AudioSource audioSource;
    //The number of seconds for each song beat
    public float secPerBeat;

    //Current song position, in seconds
    public float songPosition;

    //Current song position, in beats
    public float songPositionInBeats;

    //How many seconds have passed since the song started
    public float dspSongTime;

    public float totalBeats;

    bool isMusicPlayed = false;

    public void Start()
    {
        GameManager.GameStart += PlayMusic;
        //Calculate the number of seconds in each beat
        secPerBeat = 60f / LevelSettings.Instance.beatsPerMinute;

        totalBeats = audioSource.clip.length / secPerBeat;
       
    }

    public void PlayMusic()
    {
        audioSource.Play();
        dspSongTime = (float)AudioSettings.dspTime;
        isMusicPlayed = true;
    }


    void Update()
    {
        if (!isMusicPlayed)
        {
            return;
        }
      
        //determine how many seconds since the song started
        songPosition = (float)(AudioSettings.dspTime - dspSongTime);

        //determine how many beats since the song started
        songPositionInBeats = songPosition / secPerBeat;
    }

    public float GetPositionInBeats()
    {
        return songPositionInBeats;
    }

    public float GetSecondsPerBeat()
    {
        return secPerBeat;
    }


}

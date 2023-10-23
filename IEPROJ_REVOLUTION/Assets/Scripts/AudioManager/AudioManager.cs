using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    private void Awake()
    {
        
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }

        DontDestroyOnLoad(this.gameObject);
        
     
    }

    public AudioClip[] audioClips;

    private AudioSource audioSource;
    //The number of seconds for each song beat
    public float secPerBeat;

    //Current song position, in seconds
    public float songPosition;

    //Current song position, in beats
    public float songPositionInBeats;

    //How many seconds have passed since the song started
    public float dspSongTime;

    public float totalBeats;

    public LevelSettings currentLevel;


    bool isMusicPlayed = false;

    
    public void Start()
    {
        SceneManager.sceneLoaded += Setup;
        GameManager.GameStart += PlayMusic;

        audioSource = GetComponent<AudioSource>();

        // temp
        audioSource.clip = currentLevel.levelClip;

        secPerBeat = 60f / currentLevel.beatsPerMinute;

        totalBeats = audioSource.clip.length / secPerBeat;
    }

    void Setup(Scene scene, LoadSceneMode mode)
    {

        audioSource.Stop();

        //currentLevel = GameObject.FindWithTag("LevelSettings").GetComponent<LevelSettings>();

        audioSource.clip = currentLevel.levelClip;

        secPerBeat = 60f / currentLevel.beatsPerMinute;

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

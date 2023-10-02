using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource audioSource;

    public void Start()
    {
        GameManager.GameStart += PlayMusic;
    }

    public void PlayMusic()
    {
        audioSource.Play();
    }

    
}

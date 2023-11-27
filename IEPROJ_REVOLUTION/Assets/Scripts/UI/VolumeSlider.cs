using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    private AudioSource audiosource;
    public Slider volumeSlider;
    void Start()
    {
        audiosource = GameObject.Find("AudioManager").GetComponent<AudioSource>();
        volumeSlider.value = audiosource.volume;
    }

    // Update is called once per frame
    void Update()
    {
        audiosource.volume = volumeSlider.value;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualizerBase : MonoBehaviour
{
    public float bias;
    public float timeStep;
    public float timeToBeat;
    public float restSmoothTime;

    public float previousAudioValue;
    private float audioValue;
    private float timer;

    protected bool isBeat;

    public virtual void OnBeat()
    {
        timer = 0;
        isBeat = true;
    }

    public virtual void OnUpdate()
    {
        previousAudioValue = audioValue;
        audioValue = AudioSpectrum.spectrumValue;
        

        if (previousAudioValue > bias && audioValue <= bias)
        {
            if (timer > timeStep)
            {
                OnBeat();
            }
        }

        if (previousAudioValue <= bias &&  audioValue > bias)
        {
            if (timer > timeStep)
            {
                OnBeat();
            }
        }

        timer += Time.deltaTime;


    }

    private void Update()
    {
        OnUpdate();
    }
}

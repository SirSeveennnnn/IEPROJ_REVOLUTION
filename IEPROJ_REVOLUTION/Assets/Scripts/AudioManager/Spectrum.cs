using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spectrum : MonoBehaviour
{
    [SerializeField] private float spectrumMod = 1;
    [SerializeField] private float threshold = 1;

    [SerializeField] private float _pulseSize = 1.15f;
    [SerializeField] private float _returnSpeed = 5f;
    private Vector3 _startSize;
    float ticks = 0;
    float duration = 0.25f;

    //Quaternion rot;

    // Start is called before the first frame update
    void Start()
    {
        _startSize = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        ticks += Time.deltaTime;

        if (ticks > duration && FillSpectrum())
        {
            ticks = 0;
        }
        transform.localScale = Vector3.Lerp(transform.localScale, _startSize, Time.deltaTime * _returnSpeed);
    }

    bool FillSpectrum()
    {
        float[] spectrum = new float[256];

        AudioListener.GetSpectrumData(spectrum, 0, FFTWindow.Rectangular);

        for (int i = 0; i < spectrum.Length; i++)
        {
            float tmp = spectrum[i] * spectrumMod;
            if (tmp >= threshold)
            {
                //gameObject.transform.rotation = Quaternion.Euler(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));
                Pulse();
                return true;
            }
            //Debug.Log(tmp);
        }
        return false;
    }

    public void Pulse()
    {
        Debug.Log("Pulse");
        transform.localScale = _startSize * _pulseSize;
    }
}

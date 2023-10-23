using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialVisualizer : VisualizerBase
{
    [SerializeField] private Material mat;
    private Color startColor;
    [SerializeField] private float pulseIntensity = 5f;
    [SerializeField] private float returnSpeed = 1f;

    private void Start()
    {
        mat = GetComponent<MeshRenderer>().material;
        startColor = mat.color;
        Debug.Log("Initial Intensity = " + mat.color);
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        if (isBeat)
        {
            return;
        }
        Debug.Log("Current Intensity = " + mat.color);
        mat.color = Color.Lerp(mat.color, startColor, Time.deltaTime * returnSpeed);
        //mat.SetColor("_EmissionColor", startColor * Mathf.Lerp(mat.color.a, 0f, Time.deltaTime * returnSpeed));
    }

    public override void OnBeat()
    {
        base.OnBeat();
        PulseLight();
    }

    private void PulseLight()
    {
        mat.color = startColor * pulseIntensity;
        //mat.SetColor("_EmissionColor", startColor * pulseIntensity);
        Debug.Log("Pulse Intensity = " + mat.color);

        isBeat = false;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxVisualizer : VisualizerBase
{
    public float beatExposure = 8;
    public float restExposure = 4;

    [SerializeField] Material skyboxMaterial;

    private void Start()
    {
        skyboxMaterial = RenderSettings.skybox;
        skyboxMaterial.SetFloat("_Exposure", 1);
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        if (isBeat)
        {
            return;
        }

        skyboxMaterial.SetFloat("_Exposure", Mathf.Lerp(skyboxMaterial.GetFloat("_Exposure"), restExposure, restSmoothTime * Time.deltaTime));
    }

    public override void OnBeat()
    {
        base.OnBeat();
        StopCoroutine("ChangeExposure");
        StartCoroutine(ChangeExposure(beatExposure));
        
    }

    private IEnumerator ChangeExposure(float target)
    {
        //Debug.Log("on beat");
        float currentExposure = skyboxMaterial.GetFloat("_Exposure");
        float initial = currentExposure;
        float timer = 0;

        while (currentExposure != target)
        {
            currentExposure = Mathf.Lerp(initial, target, timer / timeToBeat);
            timer += Time.deltaTime;

            skyboxMaterial.SetFloat("_Exposure", currentExposure);

            yield return null;
        }

        isBeat = false;
    }
}

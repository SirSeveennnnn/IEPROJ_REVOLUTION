using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlindBlock : MonoBehaviour
{
    [SerializeField] Material origSkybox = null;
    [SerializeField] Material blackSkybox = null;
    [SerializeField] List<GameObject> uiObjects = new();

    [SerializeField] float blindDuration = 10f;
    [SerializeField] float blindFadeDuration = 0.5f;
    [SerializeField] float fogDensity = 0.2f;
    private Renderer renderer = null;


    private void Start()
    {
        renderer = GetComponent<Renderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            renderer.enabled = false;

            PrepareBlindEffect();
            StartCoroutine(TriggerBlindEffect());
        }
    }

    private void PrepareBlindEffect()
    {
        RenderSettings.skybox = blackSkybox;
        RenderSettings.fog = true;

        for (int i = 0; i < uiObjects.Count; i++)
        {
            uiObjects[i].SetActive(false);
        }
    }

    private void RemoveBlindEffect()
    {
        RenderSettings.skybox = origSkybox;
        RenderSettings.fog = false;

        for (int i = 0; i < uiObjects.Count; i++)
        {
            uiObjects[i].SetActive(true);
        }
    }

    private IEnumerator TriggerBlindEffect()
    {
        float elapsed = 0f;
        float coefficient = fogDensity / Mathf.Pow(blindFadeDuration, 2);

        while (elapsed < blindFadeDuration)
        {
            elapsed += Time.deltaTime;

            float x = elapsed;
            float controlVar = blindFadeDuration / 0.5f;
            float value = coefficient * x * (controlVar - x);

            RenderSettings.fogDensity = value;
            //Debug.Log("in " + value);

            yield return Time.deltaTime;
        }

        while (elapsed < blindDuration - blindFadeDuration)
        {
            elapsed += Time.deltaTime;
            yield return Time.deltaTime;
        }

        while (elapsed < blindDuration)
        {
            elapsed += Time.deltaTime;

            float x = blindFadeDuration - (blindDuration - elapsed);
            float value = -coefficient * x * x + fogDensity;

            RenderSettings.fogDensity = value;
            //Debug.Log("out " + value);

            yield return Time.deltaTime;
        }

        RemoveBlindEffect();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlindBlock : TimedEffectCollectible
{
    [Header("Blind Block Properties")]
    [SerializeField] Material origSkybox = null;
    [SerializeField] Material blackSkybox = null;
    [SerializeField] List<GameObject> uiObjects = new();
    
    [SerializeField] float blindFadeDuration = 0.5f;
    [SerializeField] float fogDensity = 0.2f;


    protected override IEnumerator TriggerEffect()
    {
        float elapsed = 0f;
        float initialFogDensity = RenderSettings.fogDensity;

        PrepareBlindEffect();

        while (elapsed < blindFadeDuration)
        {
            float x = elapsed;
            float lerpValue = Mathf.Sin((Mathf.PI * x / blindFadeDuration) / 2);
            float fogValue = Mathf.Lerp(initialFogDensity, fogDensity, lerpValue);

            RenderSettings.fogDensity = fogValue;
            //Debug.Log("ease out " + fogValue);

            elapsed += Time.deltaTime;
            yield return Time.deltaTime;
        }

        while (elapsed < effectDuration - blindFadeDuration)
        {
            elapsed += Time.deltaTime;
            yield return Time.deltaTime;
        }

        while (elapsed < effectDuration)
        {
            float x = blindFadeDuration - (effectDuration - elapsed);
            float baseNum = (x / blindFadeDuration);
            float lerpValue = Mathf.Pow(baseNum, 3);
            float fogValue = Mathf.Lerp(fogDensity, 0.0f, lerpValue);

            RenderSettings.fogDensity = fogValue;
            //Debug.Log("ease in " + fogValue);

            elapsed += Time.deltaTime;
            yield return Time.deltaTime;
        }

        RemoveBlindEffect();
        playerStatusScript.RemoveEffect(this);
        this.enabled = false;
    }

    private void PrepareBlindEffect()
    {
        RenderSettings.skybox = blackSkybox;
        RenderSettings.fog = true;

        // MAKE SOME COLLECTIBLES INVISIBLE

        for (int i = 0; i < uiObjects.Count; i++)
        {
            uiObjects[i].SetActive(false);
        }
    }

    private void RemoveBlindEffect()
    {
        RenderSettings.skybox = origSkybox;
        RenderSettings.fog = false;

        // REVERT INVISIBLE COLLECTIBLES

        for (int i = 0; i < uiObjects.Count; i++)
        {
            uiObjects[i].SetActive(true);
        }
    }
}

//https://easings.net/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlindBlock : TimedEffectCollectible
{
    [Space(10)] [Header("Blind Block Properties")]
    [SerializeField] Material origSkybox = null;
    [SerializeField] Material blackSkybox = null;
    [SerializeField] Camera playerCamera = null;
    [SerializeField] List<GameObject> uiObjects = new();
    
    [SerializeField] float blindFadeDuration = 0.5f;
    [SerializeField] float fogDensity = 0.2f;

    private float defaultFarClippingPlane;


    private void Start()
    {
        if (playerCamera == null)
        {
            playerCamera = Camera.main;
        }

        defaultFarClippingPlane = playerCamera.farClipPlane;
    }

    protected override void OnStackEffect(List<TimedEffectCollectible> effectsList)
    {
        if (effectsList.Count > 1)
        {
            DisableEffect();
        }
        else
        {
            BlindBlock effectInList = effectsList[0] as BlindBlock;

            if (effectInList.GetTimeRemaining() < this.EffectDuration)
            {
                effectInList.StopEffect();
                effectInList.DisableEffect();
                StartEffect();
            }
            else
            {
                DisableEffect();
            }
        }
    }

    protected override IEnumerator TriggerEffect()
    {
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
        DisableEffect();
    }

    private void PrepareBlindEffect()
    {
        RenderSettings.skybox = blackSkybox;
        RenderSettings.fog = true;
        playerCamera.farClipPlane = 20f;

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
        playerCamera.farClipPlane = defaultFarClippingPlane;

        // REVERT INVISIBLE COLLECTIBLES

        for (int i = 0; i < uiObjects.Count; i++)
        {
            uiObjects[i].SetActive(true);
        }
    }

    protected override void OnPlayerDeath()
    {
        if (!hasBeenCollected)
        {
            return;
        }

        if (effectCoroutine != null)
        {
            RenderSettings.fogDensity = 0f;
            RemoveBlindEffect();
            StopEffect();
        }

        OnResetCollectible();
        UnsubsribePlayerDeathEvent();
    }
}

//https://easings.net/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisibleBlock : TimedEffectCollectible
{
    [Space(10)] [Header("Invisible Block Properties")]
    [SerializeField] private Material invisibleMat = null;

    private Renderer[] playerRenderers = null;
    private Material[] origMatList = null;

    protected override void OnStackEffect(List<TimedEffectCollectible> effectsList)
    {
        if (effectsList.Count > 1)
        {
            DisableEffect();
        }
        else
        {
            InvisibleBlock effectInList = effectsList[0] as InvisibleBlock;

            if (effectInList.GetTimeRemaining() < this.EffectDuration)
            {
                effectInList.effectDuration = this.effectDuration + effectInList.elapsed;

                StopEffect();
                DisableEffect();
            }
        }
    }

    protected override IEnumerator TriggerEffect()
    {
        if (playerRenderers == null)
        {
            playerRenderers = playerObj.GetComponent<PlayerManager>().GetModelRenderer();
        }

        if (origMatList == null)
        {
            origMatList = new Material[playerRenderers.Length];
        }

        MakePlayerInvisible();

        while (elapsed < effectDuration)
        {
            elapsed += Time.deltaTime;
            yield return Time.deltaTime;
        }

        //yield return new WaitForSeconds(effectDuration);

        MakePlayerVisible();

        playerStatusScript.RemoveEffect(this);
        DisableEffect();
    }

    private void MakePlayerInvisible()
    {
        for (int i = 0; i < playerRenderers.Length; i++)
        {
            Renderer r = playerRenderers[i];

            origMatList[i] = r.material;
            r.material = invisibleMat;
            r.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        }
    }

    private void MakePlayerVisible()
    {
        for (int i = 0; i < playerRenderers.Length; i++)
        {
            Renderer r = playerRenderers[i];

            r.material = origMatList[i];
            r.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
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
            MakePlayerVisible();
            StopEffect();
        }

        OnResetCollectible();
        UnsubsribePlayerDeathEvent();
    }
}

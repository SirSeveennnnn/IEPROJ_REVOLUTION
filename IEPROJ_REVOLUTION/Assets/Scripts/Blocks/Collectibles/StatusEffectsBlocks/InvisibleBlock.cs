using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisibleBlock : TimedEffectCollectible
{
    [Space(10)] [Header("Invisible Block Properties")]
    [SerializeField] private Material invisibleMat = null;


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
        Renderer[] playerRenderers = playerObj.GetComponent<PlayerManager>().GetModelRenderer();
        Material[] origMatList = new Material[playerRenderers.Length];

        for (int i = 0; i < playerRenderers.Length; i++)
        {
            Renderer r = playerRenderers[i];

            origMatList[i] = r.material;
            r.material = invisibleMat;
            r.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        }

        while (elapsed < effectDuration)
        {
            elapsed += Time.deltaTime;
            yield return Time.deltaTime;
        }

        //yield return new WaitForSeconds(effectDuration);

        for (int i = 0; i < playerRenderers.Length; i++)
        {
            Renderer r = playerRenderers[i];

            r.material = origMatList[i];
            r.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
        }

        playerStatusScript.RemoveEffect(this);
        DisableEffect();
    }
}

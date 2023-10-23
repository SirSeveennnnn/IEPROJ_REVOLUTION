using System.Collections;
using UnityEngine;

public class InvisibleBlock : TimedEffectCollectible
{
    [SerializeField] private Material invisibleMat = null;


    protected override IEnumerator TriggerEffect()
    {
        float elapsed = 0f;

        Renderer playerRenderer = playerObj.GetComponent<Renderer>();
        Material origMat = playerRenderer.material;
        playerRenderer.material = invisibleMat;
        playerRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;

        while (elapsed < effectDuration)
        {
            elapsed += Time.deltaTime;
            yield return Time.deltaTime;
        }

        playerRenderer.material = origMat;
        playerRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
    }
}

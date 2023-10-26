using System.Collections;
using UnityEngine;

public class InvisibleBlock : TimedEffectCollectible
{
    [SerializeField] private Material invisibleMat = null;


    protected override IEnumerator TriggerEffect()
    {
        Renderer playerRenderer = playerObj.GetComponent<Renderer>();
        Material origMat = playerRenderer.material;
        playerRenderer.material = invisibleMat;
        playerRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;

        yield return new WaitForSeconds(effectDuration);

        playerRenderer.material = origMat;
        playerRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
    }
}

using System.Collections;
using UnityEngine;

public class InvisibleBlock : TimedEffectCollectible
{
    protected override IEnumerator TriggerEffect()
    {
        float elapsed = 0f;
        Renderer playerRenderer = playerObj.GetComponent<Renderer>();
        playerRenderer.enabled = false;

        while (elapsed < effectDuration)
        {
            elapsed += Time.deltaTime;
            yield return Time.deltaTime;
        }

        playerRenderer.enabled = true;
    }
}

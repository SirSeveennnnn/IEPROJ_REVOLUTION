using System.Collections;
using UnityEngine;

public class ShrunkenDownBlock : TimedEffectCollectible
{
    protected override IEnumerator TriggerEffect()
    {
        PlayerManager playerScript = playerObj.GetComponent<PlayerManager>();
        playerScript.OnShrukenDown(true);

        while (elapsed < effectDuration)
        {
            elapsed += Time.deltaTime;
            yield return Time.deltaTime;
        }

        //yield return new WaitForSeconds(effectDuration);

        playerScript.OnShrukenDown(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrunkenDownBlock : TimedEffectCollectible
{
    protected override void OnStackEffect(List<TimedEffectCollectible> effectsList)
    {
        if (effectsList.Count > 1)
        {
            DisableEffect();
        }
        else
        {
            ShrunkenDownBlock effectInList = effectsList[0] as ShrunkenDownBlock;

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
        playerManagerScript.OnShrukenDown(true);

        while (elapsed < effectDuration)
        {
            elapsed += Time.deltaTime;
            yield return Time.deltaTime;
        }

        //yield return new WaitForSeconds(effectDuration);

        playerManagerScript.OnShrukenDown(false);
        playerStatusScript.RemoveEffect(this);
        DisableEffect();
    }

    protected override void OnPlayerDeath()
    {
        if (!hasBeenCollected)
        {
            return;
        }

        if (effectCoroutine != null)
        {
            playerManagerScript.OnShrukenDown(false);
            StopEffect();
        }

        OnReset();
        UnsubsribePlayerDeathEvent();
    }
}

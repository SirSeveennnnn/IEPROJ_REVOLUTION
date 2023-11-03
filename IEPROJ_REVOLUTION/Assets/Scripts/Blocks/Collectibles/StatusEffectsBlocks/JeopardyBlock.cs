using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JeopardyBlock : TimedEffectCollectible
{
    [SerializeField] private int minSpamNumber;
    private int currentSpamNumber;

    private void Start()
    {
        currentSpamNumber = minSpamNumber;

        GestureManager.Instance.OnTapEvent += OnTap;
    }

    private void OnDisable()
    {
        GestureManager.Instance.OnTapEvent -= OnTap;
    }

    private void OnTap(object send, TapEventArgs args)
    {
        if (!hasBeenCollected)
        {
            return;
        }

        currentSpamNumber--;
        if (currentSpamNumber <= 0)
        {
            currentSpamNumber = 0;

            StopEffect();
            DisableEffect();
        }
    }

    protected override void OnStackEffect(List<TimedEffectCollectible> effectsList)
    {
        if (effectsList.Count > 1)
        {
            DisableEffect();
        }
        else
        {
            JeopardyBlock effectInList = effectsList[0] as JeopardyBlock;

            if (effectInList.GetTimeRemaining() < this.EffectDuration)
            {
                effectInList.effectDuration = this.effectDuration + effectInList.elapsed;
                effectInList.minSpamNumber += this.minSpamNumber;

                StopEffect();
                DisableEffect();
            }
        }
    }

    protected override IEnumerator TriggerEffect()
    {
        while (elapsed < effectDuration)
        {
            elapsed += Time.deltaTime;
            yield return Time.deltaTime;
        }

        PlayerManager playerScript = playerObj.GetComponent<PlayerManager>();
        playerScript.KillPlayer();

        DisableEffect();
    }
}

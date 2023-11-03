using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JackpotBlock : TimedEffectCollectible
{
    [SerializeField] private int maxSpamNumber;
    [SerializeField] private float pointsPerSpam;
    private int currentSpamNumber;


    private void Start()
    {
        currentSpamNumber = 0;

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

        currentSpamNumber++;
        if (currentSpamNumber >= maxSpamNumber)
        {
            currentSpamNumber = maxSpamNumber;
            StopEffect();
            OnJackpotEnd();
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
            JackpotBlock effectInList = effectsList[0] as JackpotBlock;

            if (effectInList.GetTimeRemaining() < this.EffectDuration)
            {
                effectInList.effectDuration = this.effectDuration + effectInList.elapsed;
                effectInList.maxSpamNumber += this.maxSpamNumber;
                effectInList.pointsPerSpam = effectInList.pointsPerSpam > this.pointsPerSpam ? effectInList.pointsPerSpam : this.pointsPerSpam;

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

        OnJackpotEnd();
    }

    private void OnJackpotEnd()
    {
        float score = currentSpamNumber * pointsPerSpam;
        // ADD SCORE
        Debug.Log("Jackpot: add score\nSpam:" + currentSpamNumber);

        DisableEffect();
    }
}

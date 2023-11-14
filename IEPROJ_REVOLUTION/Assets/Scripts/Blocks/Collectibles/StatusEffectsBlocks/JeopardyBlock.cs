using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class JeopardyBlock : TimedEffectCollectible
{
    [SerializeField] private int minSpamNumber;
    [SerializeField] private GameObject spamPanel;
    [SerializeField] private TMP_Text jeopardyText;
    private int currentSpamNumber;

    private void Start()
    {
        currentSpamNumber = minSpamNumber;

        GestureManager.Instance.OnTapEvent += OnTap;
    }

    private void OnDestroy()
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
        jeopardyText.text = "JEOPARDY: " + currentSpamNumber + "X";

        if (currentSpamNumber <= 0)
        {
            currentSpamNumber = 0;

            DisableSpamPanel();
            jeopardyText.gameObject.SetActive(false);

            StopEffect();
            DisableEffect();
        }
    }

    protected override void StartEffect()
    {
        spamPanel.SetActive(true);
        jeopardyText.gameObject.SetActive(true);

        jeopardyText.text = "JEOPARDY: " + currentSpamNumber + "X";

        effectCoroutine = StartCoroutine(TriggerEffect());
        playerStatusScript.AddEffect(this);
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
        spamPanel.SetActive(true);
        jeopardyText.gameObject.SetActive(true);

        while (elapsed < effectDuration)
        {
            elapsed += Time.deltaTime;
            yield return Time.deltaTime;
        }

        DisableSpamPanel();
        jeopardyText.gameObject.SetActive(false);
        DisableEffect();

        playerManagerScript.KillPlayer();
        playerStatusScript.RemoveEffect(this);
    }

    private void DisableSpamPanel()
    {
        if (!playerStatusScript.HasTimedEffect(EStatusEffects.Jackpot))
        {
            spamPanel.SetActive(false);
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
            DisableSpamPanel();
            jeopardyText.gameObject.SetActive(false);
            StopEffect();
        }

        currentSpamNumber = minSpamNumber;
        OnReset();
        UnsubsribePlayerDeathEvent();
    }
}

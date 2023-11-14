using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TimedEffectCollectible : Collectible
{
    
    [Space(10)] [Header("Timed Effect Properties")]
    [SerializeField] protected EStatusEffects effect = EStatusEffects.Unknown;
    [SerializeField] protected float effectDuration = 10;
    [SerializeField] protected float elapsed = 0;

    protected PlayerManager playerManagerScript;
    protected PlayerStatus playerStatusScript;
    protected Coroutine effectCoroutine;

    protected abstract void OnStackEffect(List<TimedEffectCollectible> effectsList);
    protected abstract IEnumerator TriggerEffect();
    protected abstract void OnPlayerDeath();


    public EStatusEffects Effect
    {
        get { return effect; }
        private set { effect = value; }
    }

    public float EffectDuration
    { 
        get { return effectDuration; } 
        private set { effectDuration = value; }
    }

    protected override void OnCollect()
    {
        elapsed = 0;
        playerStatusScript = playerObj.GetComponent<PlayerStatus>();

        playerManagerScript = playerObj.GetComponent<PlayerManager>();
        playerManagerScript.OnPlayerDeathEvent += OnPlayerDeath;

        List<TimedEffectCollectible> effectsList = playerStatusScript.GetCurrentTimedEffects(effect);

        if (effectsList != null && effectsList.Count > 0)
        {
            OnStackEffect(effectsList);
        }
        else
        {
            StartEffect();
        }
    }

    public override void OnReset()
    {
        hasBeenCollected = false;

        this.enabled = true;
        this.gameObject.SetActive(true);
        EnableAllRenderers();
    }

    protected virtual void StartEffect()
    {
        effectCoroutine = StartCoroutine(TriggerEffect());
        playerStatusScript.AddEffect(this);
    }

    protected virtual void StopEffect()
    {
        if (effectCoroutine != null)
        {
            StopCoroutine(effectCoroutine);
            effectCoroutine = null;
        }

        playerStatusScript.RemoveEffect(this);
    }

    protected void DisableEffect()
    {
        this.enabled = false;
        this.gameObject.SetActive(false);
    }

    protected float GetTimeRemaining()
    {
        return effectDuration - elapsed;
    }

    protected void UnsubsribePlayerDeathEvent()
    {
        playerManagerScript.OnPlayerDeathEvent -= OnPlayerDeath;
    }
}

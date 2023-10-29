using System.Collections;
using UnityEditor;
using UnityEngine;

public abstract class TimedEffectCollectible : Collectible
{
    
    [Space(10)] [Header("Timed Effect Properties")]
    [SerializeField] private EStatusEffects effect = EStatusEffects.Unknown;
    [SerializeField] protected float effectDuration = 10;
    [SerializeField] protected float elapsed = 0;

    protected PlayerStatus playerStatusScript;
    protected Coroutine effectCoroutine;

    protected abstract IEnumerator TriggerEffect();


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
        effectCoroutine = StartCoroutine(TriggerEffect());

        playerStatusScript = playerObj.GetComponent<PlayerStatus>();
        playerStatusScript.AddEffect(this);
    }

    public virtual void StopEffect()
    {
        StopCoroutine(effectCoroutine);
        playerStatusScript.RemoveEffect(this);
    }

    public float GetTimeRemaining()
    {
        return effectDuration - elapsed;
    }
}

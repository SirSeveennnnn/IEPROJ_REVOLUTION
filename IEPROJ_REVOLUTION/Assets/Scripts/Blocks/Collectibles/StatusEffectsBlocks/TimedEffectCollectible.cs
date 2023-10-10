using System.Collections;
using UnityEngine;

public abstract class TimedEffectCollectible : Collectible
{
    [SerializeField] private EStatusEffects effect = EStatusEffects.Unknown;
    [SerializeField] protected float effectDuration;

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

    private void Start()
    {
        playerStatusScript = GameManager.Instance.Player.GetComponent<PlayerStatus>();
    }

    protected override void OnCollect()
    {
        effectCoroutine = StartCoroutine(TriggerEffect());
        playerStatusScript.AddEffect(this);
    }

    public virtual void StopEffect()
    {
        StopCoroutine(effectCoroutine);
        playerStatusScript.RemoveEffect(this);
    }
}

using UnityEngine;

public abstract class PersistentEffectCollectible : Collectible
{
    [Space(10)] [Header("Persistent Effect Properties")]
    [SerializeField] private EStatusEffects effect = EStatusEffects.Unknown;
    [SerializeField] private EEffectTerminalEvents terminalEvent = EEffectTerminalEvents.Unknown;
    [SerializeField] protected float extraPoints;

    protected PlayerStatus playerStatusScript;


    public EStatusEffects Effect
    {
        get { return effect; }
        private set { effect = value; }
    }

    public EEffectTerminalEvents TerminalEvent
    {
        get { return terminalEvent; }
        private set { terminalEvent = value; }
    }

    public virtual void AddExtraPoint()
    {

    }
}

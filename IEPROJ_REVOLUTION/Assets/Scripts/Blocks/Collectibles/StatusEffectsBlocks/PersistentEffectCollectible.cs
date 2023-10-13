using UnityEngine;

public abstract class PersistentEffectCollectible : Collectible
{
    [SerializeField] private EStatusEffects effect = EStatusEffects.Unknown;
    [SerializeField] protected float extraPoints;
    protected PlayerStatus playerStatusScript;

    public abstract void AddExtraPoint();


    public EStatusEffects Effect
    {
        get { return effect; }
        private set { effect = value; }
    }

    private void Start()
    {
        //playerStatusScript = GameManager.Instance.Player.GetComponent<PlayerStatus>();
    }
}

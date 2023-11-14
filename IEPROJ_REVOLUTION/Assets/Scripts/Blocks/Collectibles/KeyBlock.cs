using System;

public class KeyBlock : Collectible
{
    public event Action OnKeyCollectedEvent;


    public bool HasBeenCollected
    {
        get { return hasBeenCollected; }
        private set { hasBeenCollected = value; }
    }

    protected override void OnCollect()
    {
        OnKeyCollectedEvent.Invoke();

        this.enabled = false;
        this.gameObject.SetActive(false);
    }

    public override void OnResetCollectible()
    {
        hasBeenCollected = false;

        this.enabled = true;
        this.gameObject.SetActive(true);
        EnableAllRenderers();
    }
}

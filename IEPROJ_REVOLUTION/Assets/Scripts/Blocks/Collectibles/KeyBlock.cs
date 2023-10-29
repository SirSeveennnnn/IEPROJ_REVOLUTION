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
    }
}

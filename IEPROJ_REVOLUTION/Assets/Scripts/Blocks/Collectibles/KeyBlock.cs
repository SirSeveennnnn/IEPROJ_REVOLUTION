using System;
using System.Collections.Generic;
using UnityEngine;

public class KeyBlock : Collectible
{
    [SerializeField] private bool hasBeenCollected;
    public event Action OnKeyCollectedEvent;


    public bool HasBeenCollected
    {
        get { return hasBeenCollected; }
        private set { hasBeenCollected = value; }
    }

    protected override void OnCollect()
    {
        hasBeenCollected = true;
        OnKeyCollectedEvent.Invoke();
    }
}

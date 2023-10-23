using System;
using System.Collections.Generic;
using UnityEngine;

public class KeyBlock : Collectible
{
    [SerializeField] private bool hasBeenCollected = false;
    public event Action OnKeyCollectedEvent;


    public bool HasBeenCollected
    {
        get { return hasBeenCollected; }
        private set { hasBeenCollected = value; }
    }

    protected override void OnCollect()
    {
        if(HasBeenCollected)
        {
            return;
        }

        hasBeenCollected = true;
        OnKeyCollectedEvent.Invoke();
    }
}

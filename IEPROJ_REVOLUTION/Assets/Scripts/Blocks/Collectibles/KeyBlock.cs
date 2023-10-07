using System.Collections.Generic;
using UnityEngine;

public class KeyBlock : Collectible
{
    [SerializeField] private List<LockedBlock> lockedBlockRefList;

    protected override void OnCollect()
    {
        foreach (var lockedBlock in lockedBlockRefList)
        {
            lockedBlock.AddKey(this);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalHandler : MonoBehaviour, IResettable
{
    public List<Portal> portals;

    public bool isUsed = false;

    public void OnReset()
    {
        isUsed = false;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyBlock : MonoBehaviour
{
    public GateBlock gateBlock;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gateBlock.CheckToUnlock();
            this.gameObject.SetActive(false);
            
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class GateBlock : MonoBehaviour
{
    [Header("Lock And Key Properties")]
    public List<KeyBlock> keyList = new();

    [Header("Unlock Properties")]
    [SerializeField] private float unlockDuration = 0.25f;
    [SerializeField] private float moveDistance = 2f;
    [SerializeField] private int keysCollected = 0;

    private Vector3[] targetY = new Vector3[3];

    private void Start()
    {
        keysCollected = -1;

        foreach (KeyBlock key in keyList)
        {
            key.gateBlock = this;
        }

        for (int i = 0; i < targetY.Length; i++)
        {
            targetY[i] = transform.position;
        }

        targetY[0].y -= 1;
        targetY[1].y -= 2;
        targetY[2].y -= 4.2f;

        Debug.Log(targetY[0]);
        Debug.Log(targetY[1]);
        Debug.Log(targetY[2]);

    }

    public void CheckToUnlock()
    {
        keysCollected++;

        StartCoroutine(UnlockBlock());
    }

    private IEnumerator UnlockBlock()
    {
        float elapsedTime = 0;
        Vector3 startPos = transform.position;

        while (elapsedTime < 2)
        {

            transform.position = Vector3.Lerp(transform.position, targetY[keysCollected], (elapsedTime / 2));
            elapsedTime += Time.deltaTime;

            yield return Time.deltaTime;
        }

        transform.position = targetY[keysCollected];

    }

}

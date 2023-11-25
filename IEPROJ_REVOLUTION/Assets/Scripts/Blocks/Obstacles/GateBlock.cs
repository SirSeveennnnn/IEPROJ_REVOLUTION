using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class GateBlock : MonoBehaviour, IResettable
{
    [Header("Lock And Key Properties")]
    [SerializeField] private List<KeyBlock> keyList = new();

    [Header("Unlock Properties")]
    [SerializeField] private float unlockDuration = 0.25f;
    [SerializeField] private float moveDistance = 2f;
    [SerializeField] private int keysCollected = 0;
    private float startYPos;
    private float targetYPos;
    private float defaultYPos;

    private Collider col;
    private Rigidbody rb;
    private Coroutine unlockCoroutine;


    private void Start()
    {
        foreach (var key in keyList)
        {
            key.OnKeyCollectedEvent += CheckToUnlock;
        }

        keysCollected = 0;
        defaultYPos = transform.position.y;

        tag = "Obstacle";

        col = GetComponent<Collider>();
        rb = GetComponent<Rigidbody>();

        col.isTrigger = true;
        rb.useGravity = false;
        rb.isKinematic = true;
    }

    private void OnDestroy()
    {
        foreach (var key in keyList)
        {
            key.OnKeyCollectedEvent -= CheckToUnlock;
        }
    }

    private void CheckToUnlock()
    {
        keysCollected++;

        float localMoveDistance = this.moveDistance;

        if (keysCollected == keyList.Count)
        {
            localMoveDistance *= 2.5f;
        }

        if (unlockCoroutine == null)
        {
            startYPos = transform.position.y;
            targetYPos = startYPos - localMoveDistance;
        }
        else
        {
            startYPos = transform.position.y;
            targetYPos -= localMoveDistance;
            StopCoroutine(unlockCoroutine);
        }

        unlockCoroutine = StartCoroutine(UnlockBlock());
    }

    private IEnumerator UnlockBlock()
    {
        float elapsed = 0f;

        while (elapsed < unlockDuration)
        {
            elapsed += Time.deltaTime;
            transform.position = new Vector3(transform.position.x, Mathf.Lerp(startYPos, targetYPos, elapsed / unlockDuration), transform.position.z);

            yield return Time.deltaTime;
        }

        transform.position = new Vector3(transform.position.x, targetYPos, transform.position.z);
        this.enabled = false;
    }

    public void OnReset()
    {
        if (unlockCoroutine != null)
        {
            StopCoroutine(unlockCoroutine);
            unlockCoroutine = null;
        }

        keysCollected = 0;
        transform.position = new Vector3(transform.position.x, defaultYPos, transform.position.z);
        this.enabled = true;
    }
}

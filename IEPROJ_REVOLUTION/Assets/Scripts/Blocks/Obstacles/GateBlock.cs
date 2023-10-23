using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateBlock : MonoBehaviour
{
    [Header("Lock And Key Properties")]
    [SerializeField] private List<KeyBlock> keyList = new();

    [Header("Unlock Properties")]
    [SerializeField] private float unlockDuration = 0.25f;
    [SerializeField] private float moveDistance = 2f;
    [SerializeField] private int keysCollected = 0;
    private float startYPos;
    private float targetYPos;

    private Renderer r;
    private Coroutine unlockCoroutine;


    private void Awake()
    {
        r = GetComponent<Renderer>();
    }

    private void Start()
    {
        foreach (var key in keyList)
        {
            key.OnKeyCollectedEvent += CheckToUnlock;
        }
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

        if (keysCollected == keyList.Count)
        {
            moveDistance *= 2.5f;
        }

        if (unlockCoroutine == null)
        {
            startYPos = transform.position.y;
            targetYPos = startYPos - moveDistance;
        }
        else
        {
            startYPos = transform.position.y;
            targetYPos -= moveDistance;
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
}

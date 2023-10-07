using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedBlock : MonoBehaviour
{
    [Header("Lock And Key Properties")]
    [SerializeField] private uint numKeyRequirements = 3;
    [SerializeField] private List<KeyBlock> keysCollectedList = new();

    [Header("Unlock Properties")]
    [SerializeField] private Material unlockedMaterial = null;
    private Renderer r;

    [SerializeField] private float unlockDuration = 0.25f;
    [SerializeField] private float moveDistance = 2f;
    private float startYPos;
    private float targetYPos;

    private Coroutine unlockCoroutine;


    private void Awake()
    {
        r = GetComponent<Renderer>();
    }

    public void AddKey(KeyBlock key)
    {
        if (unlockCoroutine != null)
        {
            return;
        }

        if (!keysCollectedList.Contains(key))
        {
            keysCollectedList.Add(key);
        }

        if (keysCollectedList.Count >= numKeyRequirements)
        {
            unlockCoroutine = StartCoroutine(UnlockBlock());
        }
    }

    private IEnumerator UnlockBlock()
    {
        r.material = unlockedMaterial;

        float elapsed = 0f;
        startYPos = transform.position.y;
        targetYPos = startYPos - moveDistance;

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

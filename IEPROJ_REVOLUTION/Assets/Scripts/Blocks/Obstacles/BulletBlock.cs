using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class BulletBlock : MonoBehaviour
{
    private Collider col;
    private Rigidbody rb;
    private Coroutine bulletCoroutine;

    private void Start()
    {
        if (GetComponent<StompableBlock>() == null)
        {
            tag = "Obstacle";
        }

        bulletCoroutine = null;

        col = GetComponent<Collider>();
        rb = GetComponent<Rigidbody>();

        col.isTrigger = true;
        rb.useGravity = false;
        rb.isKinematic = true;
    }

    public void TriggerBullet(float speed, GameObject playerObj, GameObject cannonObj)
    {
        if (bulletCoroutine != null)
        {
            return;
        }

        bulletCoroutine = StartCoroutine(TriggerBulletCoroutine(speed, playerObj, cannonObj));
    }

    private IEnumerator TriggerBulletCoroutine(float speed, GameObject playerObj, GameObject cannonObj)
    {
        while ((transform.position.z - playerObj.transform.position.z) > -5f)
        {
            transform.localPosition = new Vector3 (transform.localPosition.x, transform.localPosition.y, transform.localPosition.z + speed * Time.deltaTime);

            yield return null;
        }

        cannonObj.SetActive(false);
    }

    public void OnReset()
    {
        if (bulletCoroutine != null)
        {
            StopCoroutine(bulletCoroutine);
            bulletCoroutine = null;
        }

        this.transform.localPosition = Vector3.zero;
    }
}

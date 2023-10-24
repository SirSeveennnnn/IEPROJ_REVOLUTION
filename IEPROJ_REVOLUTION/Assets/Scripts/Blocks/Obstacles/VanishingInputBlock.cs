using UnityEngine;

[RequireComponent(typeof(Renderer))]
[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class VanishingInputBlock : MonoBehaviour
{
    private Renderer r;
    private Collider col;
    private Rigidbody rb;

    private void Start()
    {
        r = GetComponent<Renderer>();
        col = GetComponent<Collider>();
        rb = GetComponent<Rigidbody>();

        col.isTrigger = true;
        rb.useGravity = false;
        rb.isKinematic = true;
    }

    private void Update()
    {
        if (SwipeManager.tap || SwipeManager.swipeRight || SwipeManager.swipeLeft || SwipeManager.swipeUp || SwipeManager.swipeDown)
        {
            r.enabled = !r.enabled;
            col.enabled = !col.enabled;
        }
    }
}

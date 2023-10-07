using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public abstract class Collectible : MonoBehaviour
{
    [Header("Collectible Properties")]
    [SerializeField] private bool isVisible = false;

    private Renderer r = null;
    private Collider c = null;
    private Rigidbody rb = null;

    protected abstract void OnCollect();


    private void Awake()
    {
        gameObject.tag = "Collectible";

        r = GetComponent<Renderer>();
        r.enabled = isVisible;

        c = GetComponent<Collider>();
        c.isTrigger = true;

        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.isKinematic = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            r.enabled = false;

            OnCollect();
        }
    }
}

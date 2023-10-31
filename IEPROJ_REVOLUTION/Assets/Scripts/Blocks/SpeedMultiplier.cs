using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class SpeedMultiplier : MonoBehaviour
{
    [SerializeField] private float speedMultiplier;
    private Collider col;
    private Rigidbody rb;

    private void Start()
    {
        col = GetComponent<Collider>();
        rb = GetComponent<Rigidbody>();

        col.isTrigger = true;
        rb.useGravity = false;
        rb.isKinematic = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerMovement movementScript = other.GetComponent<PlayerMovement>();
            movementScript.ChangePlayerSpeed(speedMultiplier);

            this.enabled = false;
        }
    }
}

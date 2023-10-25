using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class MagnetBlock : MonoBehaviour
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerMovement movementScript = other.GetComponent<PlayerMovement>();
            movementScript.PlayerMove(transform.position.x, 0.08f);

            PlayerManager playerScript = other.GetComponent<PlayerManager>();
            playerScript.ApplyIFrames(0.3f);

            r.enabled = false;
            this.enabled = false;
        }
    }
}

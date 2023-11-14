using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class MagnetBlock : MonoBehaviour, IResettable
{
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
            movementScript.PlayerMove(transform.position.x, 0.05f);

            PlayerManager playerScript = other.GetComponent<PlayerManager>();
            playerScript.ApplyIFrames(0.3f);

            this.enabled = false;
            this.gameObject.SetActive(false);
        }
    }

    public void OnReset()
    {
        this.enabled = true;
        this.gameObject.SetActive(true);
    }
}

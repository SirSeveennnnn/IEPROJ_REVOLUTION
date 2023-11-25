using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class ForceMoveBlock : DistanceBasedBlock
{
    [SerializeField] private bool isGoingRight;

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
            float movePos = other.transform.position.x;
            movePos += isGoingRight ? levelSettings.laneDistance : -levelSettings.laneDistance;

            PlayerMovement movementScript = other.GetComponent<PlayerMovement>();
            movementScript.PlayerMove(movePos);
        }
    }
}

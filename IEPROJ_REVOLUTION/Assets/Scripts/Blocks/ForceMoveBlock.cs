using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class ForceMoveBlock : MonoBehaviour
{
    [SerializeField] private bool isGoingRight;

    public float moveDistance;

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
            float movePos = isGoingRight ? moveDistance : -moveDistance;
            PlayerMovement movementScript = other.GetComponent<PlayerMovement>();
            movementScript.MovePlayer(movePos);
        }
    }
}

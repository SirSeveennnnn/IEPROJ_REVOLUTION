using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class JumpPad : MonoBehaviour
{
    [SerializeField] private float jumpDuration;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerMovement movementScript = other.GetComponent<PlayerMovement>();

        }
    }
}

using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class JumpPad : MonoBehaviour
{
    [SerializeField] private float jumpDuration;
    [SerializeField] private AnimationCurve jumpCurve;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerMovement movementScript = other.GetComponent<PlayerMovement>();
            movementScript.PlayerJump(1.8f, jumpCurve);
        }
    }
}

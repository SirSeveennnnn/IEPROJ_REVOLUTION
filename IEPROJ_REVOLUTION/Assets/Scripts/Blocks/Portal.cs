using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class Portal : MonoBehaviour
{
    [SerializeField] private Transform outPortal;
    [SerializeField] private PlayerManager player;


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            player = other.GetComponent<PlayerManager>();
            //player.ApplyIFrames(0.5f);

            PlayerMovement movementScript = player.GetComponent<PlayerMovement>();
            //movementScript.Teleport(outPortal.position.x);

            this.enabled = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            player = null;
        }
    }
}

using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class Portal : MonoBehaviour
{
    public PortalHandler handler;
    public GameObject otherPortal;


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && handler.isUsed == false)
        {
            PlayerMovement movementScript = other.GetComponent<PlayerMovement>();
            movementScript.MovePlayerPosition(otherPortal.transform.position.x);
            handler.isUsed = true;
        }
    }

}

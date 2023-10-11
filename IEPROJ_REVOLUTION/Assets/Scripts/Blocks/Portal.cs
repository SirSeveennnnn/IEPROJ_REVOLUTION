using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class Portal : MonoBehaviour
{
    [SerializeField] private Transform outPortal;
    [SerializeField] private GameObject playerObj;

    private void Update()
    {
        if (playerObj != null && SwipeManager.tap)
        {
            // give player iframes

            Vector3 teleportPos = new Vector3(outPortal.position.x, playerObj.transform.position.y, playerObj.transform.position.z);
            playerObj.transform.position = teleportPos;

            this.enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            playerObj = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            playerObj = null;
        }
    }
}

using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class StompableBlock : MonoBehaviour
{
    [SerializeField] private AnimationCurve curve;
    [SerializeField] private float offset;
    [SerializeField] private int bonusPoints;

    private Collider col;
    private Rigidbody rb;

    private void Start()
    {
        tag = "Stompable";

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
            GameObject playerObj = other.gameObject;
            float diff = playerObj.transform.position.y - transform.position.y;

            if (diff > offset)
            {
                PlayerMovement movementScript = playerObj.GetComponent<PlayerMovement>();
                movementScript.PlayerStomp(curve);

                // Add score

                this.gameObject.SetActive(false);
            }
            else
            {
                PlayerManager playerScript = playerObj.GetComponent<PlayerManager>();
                playerScript.KillPlayer();
            }
        }

    }
}

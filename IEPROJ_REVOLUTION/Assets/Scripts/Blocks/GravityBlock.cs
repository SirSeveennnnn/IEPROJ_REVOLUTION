using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class GravityBlock : MonoBehaviour
{
    private bool hasSwiped;
    private Collider col;
    private Rigidbody rb;
    [SerializeField] private GameObject player;

    private void Start()
    {
        hasSwiped = false;

        GestureManager.Instance.OnSwipeEvent += OnSwipe;

        col = GetComponent<Collider>();
        rb = GetComponent<Rigidbody>();

        col.isTrigger = true;
        rb.useGravity = false;
        rb.isKinematic = true;
    }

    private void OnDisable()
    {
        GestureManager.Instance.OnSwipeEvent -= OnSwipe;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            player = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (hasSwiped)
        {
            return;
        }

        if (other.tag == "Player")
        {
            player = null;
            this.enabled = false;
        }
    }

    private void OnSwipe(object send, SwipeEventArgs args)
    {
        if (args.SwipeDirection == SwipeEventArgs.SwipeDirections.DOWN && player != null)
        {
            hasSwiped = true;

            PlayerMovement movementScript = player.GetComponent<PlayerMovement>();
            movementScript.StopPlayerActions();
            movementScript.PlayerDrop();

            PlayerManager playerScript = player.GetComponent<PlayerManager>();
            playerScript.ApplyIFrames(0.5f);

            player = null;
            this.enabled = false;
        }
    }
}

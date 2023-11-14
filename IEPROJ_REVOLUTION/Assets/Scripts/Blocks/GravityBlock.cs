using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class GravityBlock : MonoBehaviour, IResettable
{
    private bool hasSwiped;
    private Collider col;
    private Rigidbody rb;
    private GameObject player;

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

    private void OnDestroy()
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
            movementScript.PlayerDrop();

            PlayerManager playerScript = player.GetComponent<PlayerManager>();
            playerScript.ApplyIFrames(0.5f);

            player = null;
            this.enabled = false;
        }
    }

    public void OnReset()
    {
        hasSwiped = false;
        player = null;
        this.enabled = true;
    }
}

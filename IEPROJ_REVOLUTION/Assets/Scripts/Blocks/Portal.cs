using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class Portal : MonoBehaviour
{
    public PortalHandler handler;
    [SerializeField] private Transform otherPortal;
    [SerializeField] private PlayerManager player;

    //private void Start()
    //{
    //    GestureManager.Instance.OnSwipeEvent += OnSwipe;
    //}

    //private void OnDestroy()
    //{
    //    GestureManager.Instance.OnSwipeEvent -= OnSwipe;
    //}

    //private void OnSwipe(object send, SwipeEventArgs args)
    //{
    //    if (player == null)
    //    {
    //        return;
    //    }

    //    if (args.SwipeDirection == SwipeEventArgs.SwipeDirections.DOWN)
    //    {
    //        player.ApplyIFrames(0.5f);

    //        PlayerMovement movementScript = player.GetComponent<PlayerMovement>();
    //        movementScript.Teleport(outPortal.position.x);

    //        player = null;
    //        this.enabled = false;
    //    }
    //}

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.tag == "Player")
    //    {
    //        player = other.GetComponent<PlayerManager>();
    //    }
    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.tag == "Player")
    //    {
    //        player = null;
    //    }
    //}

    //public void OnReset()
    //{
    //    player = null;
    //    this.enabled = true;
    //}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && handler.isUsed == false)
        {
            PlayerMovement movementScript = other.GetComponent<PlayerMovement>();
            movementScript.PlayerMove(otherPortal.position.x);
            
            PlayerManager managerScript = other.GetComponent<PlayerManager>();
            managerScript.ApplyIFrames(0.5f);

            handler.isUsed = true;
        }
    }
}

using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class Portal : MonoBehaviour, IResettable
{
    [SerializeField] private Transform outPortal;
    [SerializeField] private PlayerManager player;

    private void Start()
    {
        //GestureManager.Instance.OnTapEvent += OnTap;
        GestureManager.Instance.OnSwipeEvent += OnSwipe;
    }

    private void OnDestroy()
    {
        //GestureManager.Instance.OnTapEvent -= OnTap;
        GestureManager.Instance.OnSwipeEvent -= OnSwipe;
    }

    //private void OnTap(object send, TapEventArgs args)
    //{
    //    if (player == null)
    //    {
    //        return;
    //    }

    //    player.ApplyIFrames(0.5f);

    //    PlayerMovement movementScript = player.GetComponent<PlayerMovement>();
    //    movementScript.Teleport(outPortal.position.x);

    //    this.enabled = false;
    //}

    private void OnSwipe(object send, SwipeEventArgs args)
    {
        if (player == null)
        {
            return;
        }

        if (args.SwipeDirection == SwipeEventArgs.SwipeDirections.DOWN)
        {
            player.ApplyIFrames(0.5f);

            PlayerMovement movementScript = player.GetComponent<PlayerMovement>();
            movementScript.Teleport(outPortal.position.x);

            player = null;
            this.enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            player = other.GetComponent<PlayerManager>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            player = null;
        }
    }

    public void OnReset()
    {
        player = null;
        this.enabled = true;
    }
}

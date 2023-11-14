using UnityEngine;

[RequireComponent(typeof(Renderer))]
[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class VanishingInputBlock : MonoBehaviour, IResettable
{
    private bool hasGameStarted;

    private Renderer r;
    private Collider col;
    private Rigidbody rb;
    private GameObject playerObj;

    private void Start()
    {
        if (GetComponent<StompableBlock>() == null)
        {
            tag = "Obstacle";
        }

        hasGameStarted = false;

        r = GetComponent<Renderer>();
        col = GetComponent<Collider>();
        rb = GetComponent<Rigidbody>();

        col.isTrigger = true;
        rb.useGravity = false;
        rb.isKinematic = true;

        playerObj = GameManager.Instance.Player.gameObject;

        GameManager.GameStartEvent += OnGameStart;
        GestureManager.Instance.OnTapEvent += OnTap;
        GestureManager.Instance.OnSwipeEvent += OnSwipe;
    }

    private void OnDestroy()
    {
        GameManager.GameStartEvent -= OnGameStart;
        GestureManager.Instance.OnTapEvent -= OnTap;
        GestureManager.Instance.OnSwipeEvent -= OnSwipe;
    }

    private void Update()
    {
        if ((transform.position.z - playerObj.transform.position.z) < -5.0f)
        {
            this.enabled = false;
            this.gameObject.SetActive(false);
        }
    }

    private void OnGameStart()
    {
        hasGameStarted = true;
    }

    private void OnTap(object send, TapEventArgs args)
    {
        if (!hasGameStarted)
        {
            return;
        }

        r.enabled = !r.enabled;
        col.enabled = !col.enabled;
    }

    private void OnSwipe(object send, SwipeEventArgs args)
    {
        if (!hasGameStarted)
        {
            return;
        }

        r.enabled = !r.enabled;
        col.enabled = !col.enabled;
    }

    public void OnReset()
    {
        r.enabled = true;
        col.enabled = true;

        this.enabled = true;
        this.gameObject.SetActive(true);
    }
}

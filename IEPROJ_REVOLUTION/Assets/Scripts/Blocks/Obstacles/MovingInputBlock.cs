using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class MovingInputBlock : MonoBehaviour, IResettable
{
    [SerializeField] private float leftPos;
    [SerializeField] private float rightPos;
    [SerializeField] private bool isGoingRight;

    private bool hasGameStarted;
    private float moveDuration;
    private float startXPos;
    private float targetXPos;
    private Coroutine moveCoroutine;

    private float defaultXPos;
    private bool defaultGoingRight;

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
        moveDuration = 0.05f;
        moveCoroutine = null;

        defaultXPos = transform.position.x;
        defaultGoingRight = isGoingRight;

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

    private void OnDisable()
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
        }
    }

    private void OnGameStart()
    {
        hasGameStarted = true;
    }

    private void OnTap(object send, TapEventArgs args)
    {
        CheckInput();
    }

    private void OnSwipe(object send, SwipeEventArgs args)
    {
        CheckInput();
    }

    private void CheckInput()
    {
        if (!hasGameStarted)
        {
            return;
        }

        if (moveCoroutine != null)
        {
            StopCoroutine(moveCoroutine);
            moveCoroutine = null;
        }

        moveCoroutine = StartCoroutine(MoveCoroutine());
    }

    private IEnumerator MoveCoroutine()
    {
        startXPos = isGoingRight ? leftPos : rightPos;
        targetXPos = isGoingRight ? rightPos : leftPos; 
        
        float distance = Mathf.Abs(rightPos - leftPos);
        float elapsed = isGoingRight ? Mathf.Abs(leftPos - transform.position.x) / distance : Mathf.Abs(rightPos - transform.position.x) / distance;
        elapsed *= moveDuration;

        while (elapsed < moveDuration)
        {
            transform.position = new Vector3(Mathf.Lerp(startXPos, targetXPos, elapsed / moveDuration), transform.position.y, transform.position.z);
            elapsed += Time.deltaTime;

            yield return Time.deltaTime;
        }

        transform.position = new Vector3(targetXPos, transform.position.y, transform.position.z);
        isGoingRight = !isGoingRight;
        moveCoroutine = null;
    }

    public void OnReset()
    {
        if (moveCoroutine != null)
        {
            StopCoroutine(moveCoroutine);
            moveCoroutine = null;
        }

        isGoingRight = defaultGoingRight;
        transform.position = new Vector3(defaultXPos, transform.position.y, transform.position.z);

        this.enabled = true;
    }
}

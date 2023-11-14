using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class MoveBlock : DistanceBasedBlock, IResettable
{
    [SerializeField] private int triggerDistanceByBlock;
    [SerializeField] private float moveDuration;
    [SerializeField, Range(-1f, 1f)] private int horizontalMovement;
    [SerializeField, Range(-1f, 1f)] private int distalMovement;

    private bool isMovementTriggered;
    private float elapsedTime;
    private Vector3 startPos;
    private Vector3 targetPos;
    private Vector3 defaultPos;

    private GameObject playerObj;
    private Collider col;
    private Rigidbody rb;

    private void Start()
    {
        isMovementTriggered = false;
        elapsedTime = 0f;
        startPos = Vector3.zero;
        targetPos = Vector3.zero;

        defaultPos = transform.position;

        if (GetComponent<StompableBlock>() == null)
        {
            tag = "Obstacle";
        }

        playerObj = GameManager.Instance.Player.gameObject;

        col = GetComponent<Collider>();
        rb = GetComponent<Rigidbody>();

        col.isTrigger = true;
        rb.useGravity = false;
        rb.isKinematic = true;
    }

    private void Update()
    {
        if (!isMovementTriggered && (transform.position.z - playerObj.transform.position.z) <= levelSettings.laneDistance * triggerDistanceByBlock)
        {
            isMovementTriggered = true;

            startPos = transform.position;
            targetPos = transform.position;

            targetPos.x += levelSettings.laneDistance * horizontalMovement;
            targetPos.z += levelSettings.laneDistance * distalMovement;
        }

        if (isMovementTriggered && elapsedTime < moveDuration)
        {
            transform.position = Vector3.Lerp(startPos, targetPos, elapsedTime / moveDuration);
            elapsedTime += Time.deltaTime;

            if (elapsedTime >= moveDuration)
            {
                transform.position = targetPos;
                this.enabled = false;
            }
        }
    }

    public void OnReset()
    {
        transform.position = defaultPos;
        isMovementTriggered = false;
        elapsedTime = 0f;

        this.enabled = true;
    }
}

using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class MoveBlock : MonoBehaviour
{
    [SerializeField] private LevelSettings levelSettings;
    [SerializeField] private int triggerDistanceByBlock;
    [SerializeField] private float moveDuration;
    [SerializeField, Range(-1f, 1f)] private int horizontalMovement;
    [SerializeField, Range(-1f, 1f)] private int distalMovement;

    private bool isMovementTriggered;
    private float elapsedTime;
    private Vector3 startPos;
    private Vector3 targetPos;

    private GameObject playerObj;
    private Collider col;
    private Rigidbody rb;

    private void Start()
    {
        isMovementTriggered = false;
        elapsedTime = 0f;
        startPos = Vector3.zero;
        targetPos = Vector3.zero;

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

    public void SetLevelSettings(LevelSettings levelSettings)
    {
        this.levelSettings = levelSettings;
    }
}

using UnityEngine;

public class MoveBlock : MonoBehaviour
{
    [SerializeField] private float laneDistance;
    [SerializeField] private int triggerDistanceByBlock;
    [SerializeField] private float moveDuration;
    [SerializeField] private bool isGoingRight;

    private bool isMovementTriggered;
    private float elapsedTime;
    private float startXPos;
    private float targetXPos;

    private GameObject playerObj;

    private void Start()
    {
        isMovementTriggered = false;
        elapsedTime = 0f;
        startXPos = 0f;
        targetXPos = 0f;
        
        playerObj = GameManager.Instance.Player;
    }

    private void Update()
    {
        if (!isMovementTriggered && Vector3.Distance(transform.position, playerObj.transform.position) <= laneDistance * triggerDistanceByBlock)
        {
            isMovementTriggered = true;

            startXPos = transform.position.x;
            targetXPos = isGoingRight ? startXPos + laneDistance : startXPos - laneDistance;
        }

        if (isMovementTriggered && elapsedTime < moveDuration)
        {
            transform.position = new Vector3(Mathf.Lerp(startXPos, targetXPos, elapsedTime / moveDuration), transform.position.y, transform.position.z);
            elapsedTime += Time.deltaTime;

            if (elapsedTime >= moveDuration)
            {
                transform.position = new Vector3(targetXPos, transform.position.y, transform.position.z);
                this.enabled = false;
            }
        }
    }
}

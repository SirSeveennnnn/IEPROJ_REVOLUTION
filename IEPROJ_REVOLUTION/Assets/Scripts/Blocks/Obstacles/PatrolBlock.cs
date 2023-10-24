using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Jobs;

public class PatrolBlock : MonoBehaviour
{
    [SerializeField] private float leftPos;
    [SerializeField] private float rightPos;
    [SerializeField] private float moveDuration;
    [SerializeField] private bool isGoingRight;

    private bool hasGameStarted;
    private float elapsedTime;
    private float startXPos;
    private float targetXPos;

    private GameObject playerObj;

    private void Start()
    {
        startXPos = isGoingRight ? leftPos : rightPos;
        targetXPos = isGoingRight ? rightPos : leftPos;

        float distance = Mathf.Abs(rightPos - leftPos);
        elapsedTime = isGoingRight ? Mathf.Abs(leftPos - transform.position.x) / distance : Mathf.Abs(rightPos - transform.position.x) / distance;
        elapsedTime *= moveDuration;

        tag = "Obstacle";

        playerObj = GameManager.Instance.Player.gameObject;
        GameManager.GameStartEvent += StartGame;
    }

    private void Update()
    {
        if (!hasGameStarted)
        {
            return;
        }

        if (elapsedTime < moveDuration)
        {
            transform.position = new Vector3(Mathf.Lerp(startXPos, targetXPos, elapsedTime / moveDuration), transform.position.y, transform.position.z);
            elapsedTime += Time.deltaTime;

            if (elapsedTime >= moveDuration || Mathf.Abs(startXPos - targetXPos) < 0.001f)
            {
                isGoingRight = !isGoingRight;
                elapsedTime = 0.0f;

                startXPos = transform.position.x;
                targetXPos = isGoingRight ? rightPos : leftPos;
            }
        }

        if (transform.position.z - playerObj.transform.position.z < -5.0f)
        {
            this.enabled = false;
        }
    }

    private void StartGame()
    {
        hasGameStarted = true;
    }
}

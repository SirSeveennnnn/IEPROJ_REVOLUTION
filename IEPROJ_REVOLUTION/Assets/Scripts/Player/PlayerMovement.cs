using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Settings")]
    [SerializeField] private float playerSpeed = 0;
    [SerializeField] private float rotationSpeedMultiplier = 0;
    [SerializeField] private int bpmMultiplier;

    [Header("Jump Settings")]
    [SerializeField] private AnimationCurve jumpCurve;
    [SerializeField] private int jumpDistance = 3;


    [Space(10)]   // lerp stuff
    private float laneChangeDuration = 0.03f;
    private float startXPos;
    private float endXPos;

    [Header("Other Properties")]
    [SerializeField] private int currentLane = 3;
    [SerializeField] private LevelSettings levelSettings;

    private PlayerManager playerManager;
    private PlayerAnimation playerAnimation;
    private bool playerStart = false;
    private bool isInAction = false;
    private Coroutine actionCoroutine = null;


    void Start()
    {
        GameManager.GameStart += StartPlayer;
        playerManager = GetComponent<PlayerManager>();
        playerAnimation = GetComponent<PlayerAnimation>();

        playerManager.OnPlayerDeathEvent += StopPlayer;
        playerManager.OnPlayerWinEvent += StopPlayer;

        //playerSpeed = levelSettings.beatsPerMinute / bpmMultiplier;
        //jumpDistanceInSeconds = jumpDistance * AudioManager.Instance.GetSecondsPerBeat();
    }

    void Update()
    {
        if (!playerStart)
        {
            return;
        }

        if (SwipeManager.swipeRight)
        {
            float nextPos = transform.position.x + levelSettings.laneDistance;
            PlayerMove(nextPos);
        }
        else if (SwipeManager.swipeLeft)
        {
            float nextPos = transform.position.x - levelSettings.laneDistance;
            PlayerMove(nextPos);
        }
        else if (SwipeManager.swipeUp)
        {
            PlayerJump(jumpDistance * AudioManager.Instance.GetSecondsPerBeat());
        }

        transform.position = new Vector3(transform.position.x, transform.position.y, AudioManager.Instance.GetPositionInBeats());

        //transform.Rotate(new Vector3(Time.deltaTime * rotationSpeedMultiplier, 0, 0));
    }

    private void StartPlayer()
    {
        playerStart = true;
    }

    private void StopPlayer()
    {
        playerStart = false;
    }

    public void PlayerMove(float xPos)
    {
        if (isInAction)
        {
            return;
        }

        if ((transform.position.x + (levelSettings.numberOfRows - currentLane) * levelSettings.laneDistance) < xPos || 
            transform.position.x - (currentLane - 1) * levelSettings.laneDistance > xPos)
        {
            return;
        }

        startXPos = transform.position.x;
        endXPos = xPos;
        isInAction = true;

        int laneDiff = (int)((endXPos - startXPos) / levelSettings.laneDistance);
        currentLane += laneDiff;

        actionCoroutine = StartCoroutine(PlayerMoveCoroutine(xPos));
    }

    private IEnumerator PlayerMoveCoroutine(float xPos)
    {
        float elapsed = 0.0f;

        while (elapsed < laneChangeDuration)
        {
            transform.position = new Vector3(Mathf.Lerp(startXPos, endXPos, elapsed / laneChangeDuration), transform.position.y, transform.position.z);
            elapsed += Time.deltaTime;

            yield return Time.deltaTime;
        }

        transform.position = new Vector3(endXPos, transform.position.y, transform.position.z);
        isInAction = false;
        actionCoroutine = null;
    }

    public void PlayerJump(float duration)
    {
        if (isInAction)
        {
            return;
        }

        isInAction = true;
        actionCoroutine = StartCoroutine(PlayerJumpCoroutine(duration));
    }

    private IEnumerator PlayerJumpCoroutine(float duration)
    {
        float elapsedTime = 0f;
        float startingYPos = transform.position.y;

        playerAnimation.ToggleRoll();

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float percent = Mathf.Clamp01(elapsedTime / duration);

            transform.position = new Vector3(transform.position.x, startingYPos + jumpCurve.Evaluate(percent), transform.position.z);

            yield return null;
        }

        playerAnimation.ToggleRoll();

        transform.position = new Vector3(transform.position.x, startingYPos, transform.position.z);
        isInAction = false;
        actionCoroutine = null;
    }

    public void StopPlayerActions()
    {
        isInAction = false;
        StopCoroutine(actionCoroutine);
    }
}

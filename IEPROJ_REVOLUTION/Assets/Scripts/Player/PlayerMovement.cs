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
    private float dropDuration = 0.05f;
    private float defaultYPos;
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
        GameManager.GameStartEvent += StartPlayer;
        playerManager = GetComponent<PlayerManager>();
        playerAnimation = GetComponent<PlayerAnimation>();

        playerManager.OnPlayerDeathEvent += StopPlayer;
        playerManager.OnPlayerWinEvent += StopPlayer;

        GestureManager.Instance.OnSwipeEvent += OnSwipe;

        defaultYPos = transform.position.y;

        //playerSpeed = levelSettings.beatsPerMinute / bpmMultiplier;
        //jumpDistanceInSeconds = jumpDistance * AudioManager.Instance.GetSecondsPerBeat();
    }

    private void OnDisable()
    {
        playerManager.OnPlayerDeathEvent -= StopPlayer;
        playerManager.OnPlayerWinEvent -= StopPlayer;

        GestureManager.Instance.OnSwipeEvent -= OnSwipe;
    }

    void Update()
    {
        if (!playerStart)
        {
            return;
        }

        //if (SwipeManager.swipeRight)
        //{
        //    float nextPos = transform.position.x + levelSettings.laneDistance;
        //    PlayerMove(nextPos);
        //}
        //else if (SwipeManager.swipeLeft)
        //{
        //    float nextPos = transform.position.x - levelSettings.laneDistance;
        //    PlayerMove(nextPos);
        //}
        //else if (SwipeManager.swipeUp)
        //{
        //    PlayerJump(jumpDistance * AudioManager.Instance.GetSecondsPerBeat());
        //}

        transform.position = new Vector3(transform.position.x, transform.position.y, AudioManager.Instance.GetPositionInBeats());

        transform.Rotate(new Vector3(4 * Time.deltaTime * rotationSpeedMultiplier, 0, 0));
    }

    private void StartPlayer()
    {
        playerStart = true;
    }

    private void StopPlayer()
    {
        playerStart = false;
    }

    private void OnSwipe(object send, SwipeEventArgs args)
    {
        if (!playerStart)
        {
            return;
        }

        if (args.SwipeDirection == SwipeEventArgs.SwipeDirections.RIGHT)
        {
            float nextPos = transform.position.x + levelSettings.laneDistance;
            PlayerMove(nextPos);
        }
        else if (args.SwipeDirection == SwipeEventArgs.SwipeDirections.LEFT)
        {
            float nextPos = transform.position.x - levelSettings.laneDistance;
            PlayerMove(nextPos);
        }
        else if (args.SwipeDirection == SwipeEventArgs.SwipeDirections.UP)
        {
            PlayerJump(jumpDistance * AudioManager.Instance.GetSecondsPerBeat());
        }
    }

    public void PlayerMove(float xPos)
    {
        if (isInAction)
        {
            return;
        }

        float rightLimit = transform.position.x + (levelSettings.numberOfRows - currentLane) * levelSettings.laneDistance;
        float leftLimit = transform.position.x - (currentLane - 1) * levelSettings.laneDistance;
        if (rightLimit < xPos || leftLimit > xPos)
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

    public void PlayerJump(float duration, AnimationCurve inputCurve = null)
    {
        if (isInAction)
        {
            return;
        }

        isInAction = true;
        actionCoroutine = StartCoroutine(PlayerJumpCoroutine(duration));
    }

    private IEnumerator PlayerJumpCoroutine(float duration, AnimationCurve inputCurve = null)
    {
        float elapsedTime = 0f;
        float startingYPos = transform.position.y;

        //playerAnimation.ToggleRoll();

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float percent = Mathf.Clamp01(elapsedTime / duration);

            if (inputCurve == null)
            {
                transform.position = new Vector3(transform.position.x, startingYPos + jumpCurve.Evaluate(percent), transform.position.z);
            }
            else
            {
                transform.position = new Vector3(transform.position.x, startingYPos + inputCurve.Evaluate(percent), transform.position.z);
            }

            yield return null;
        }

        //playerAnimation.ToggleRoll();

        transform.position = new Vector3(transform.position.x, startingYPos, transform.position.z);
        isInAction = false;
        actionCoroutine = null;
    }

    public void PlayerDrop()
    {
        if (isInAction)
        {
            return;
        }

        isInAction = true;
        actionCoroutine = StartCoroutine(PlayerDropCoroutine());
    }

    private IEnumerator PlayerDropCoroutine()
    {
        float elapsedTime = 0f;
        float startingYPos = transform.position.y;

        while (elapsedTime < dropDuration)
        {
            transform.position = new Vector3(transform.position.x, Mathf.Lerp(startingYPos, defaultYPos, elapsedTime / dropDuration), transform.position.z);
            elapsedTime += Time.deltaTime;

            yield return Time.deltaTime;
        }

        //playerAnimation.ToggleRoll();

        transform.position = new Vector3(transform.position.x, defaultYPos, transform.position.z);
        isInAction = false;
        actionCoroutine = null;
    }

    public void StopPlayerActions()
    {
        isInAction = false;
        StopCoroutine(actionCoroutine);
    }

    public void Teleport(float xPos)
    {
        int laneDiff = (int)((xPos - transform.position.x) / levelSettings.laneDistance);
        currentLane += laneDiff;

        transform.position = new Vector3(xPos, transform.position.y, transform.position.z);
    }
}

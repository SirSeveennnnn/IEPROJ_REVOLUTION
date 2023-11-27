using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    private bool playerStart = false;

    [Header("Player Settings")]
    [SerializeField] private int lane = 2;
    [SerializeField] private float playerSpeed = 0;
    [SerializeField] private float rotationSpeedMultiplier = 0;
    [SerializeField] private int bpmMultiplier;
    [SerializeField] private bool isPlayerDead = false;

    [Header("Jump Settings")]
    [SerializeField] private AnimationCurve jumpCurve;
    [SerializeField] private int jumpDistance = 2;

    [Header("OtherGOs")]
    //[SerializeField] private GameObject playerModel;
    [SerializeField] private ParticleSystem sparkEffect;
    private PlayerAnimation playerAnim;


    [Space(10)]
    //lerp stuff
    [SerializeField] private float laneDistance = 1.8f;
    private float elapsedTime = 0;
    private float laneChangeDuration = 0.03f;
    private bool isLaneChanging = false;
    private float startXPos;
    private float endXPos;

    //Events
    public static event Action PlayerDeath;
    public static event Action PlayerWin;

    public ScoreText scoreText;



    private int collisionCount = 0;
    private bool isInvulnerable = false;

    public float invulnerabilityDuration = 2f;

    public event System.Action PlayerDead;






    // Start is called before the first frame update
    void Start()
    {

        playerAnim = GetComponent<PlayerAnimation>();

        GameManager.GameStart += StartPlayer;

       
    }

    // Update is called once per frame
    void Update()
    {
        if (!playerStart || isPlayerDead)
        {
            return;
        }

        if ((SwipeManager.tapRight) && (!isLaneChanging && lane != 4))
        {
            //Debug.Log("Swipe Right");
            lane++;
            startXPos = transform.position.x;
            endXPos = transform.position.x + laneDistance;
            isLaneChanging = true;
         
        }
        else if ((SwipeManager.tapLeft) && (!isLaneChanging && lane != 0))
        {
            //Debug.Log("Swipe Left");
            lane--;
            startXPos = transform.position.x;
            endXPos = transform.position.x - laneDistance;
            isLaneChanging = true;
        
        }
        else if (SwipeManager.swipeUp)
        {
            //Debug.Log("Jump");
            StartCoroutine(Jump(jumpDistance * AudioManager.Instance.GetSecondsPerBeat()));
        }

        if (isLaneChanging && elapsedTime < laneChangeDuration)
        {
            transform.position = new Vector3(Mathf.Lerp(startXPos, endXPos, elapsedTime / laneChangeDuration), transform.position.y, transform.position.z);
            elapsedTime += Time.deltaTime;

            if (elapsedTime >= laneChangeDuration)
            {
                transform.position = new Vector3(endXPos, transform.position.y, transform.position.z);
                isLaneChanging = false;
                elapsedTime = 0;
         
            }
        }

        transform.position = new Vector3(transform.position.x, transform.position.y, AudioManager.Instance.GetPositionInBeats());

    }

    private void StartPlayer()
    {
        playerStart = true;
    }

    IEnumerator Jump(float duration)
    {
        playerAnim.PlayJumpAnimation();
        

        float elapsedTime = 0f;
        float startingYPos = transform.position.y;
        while (elapsedTime < duration)
        {
            elapsedTime = elapsedTime + Time.deltaTime;
            float percent = Mathf.Clamp01(elapsedTime / duration);
           
            transform.position = new Vector3(transform.position.x, startingYPos + jumpCurve.Evaluate(percent), transform.position.z);

            yield return null;
        }

        transform.position = new Vector3(transform.position.x, startingYPos, transform.position.z);

        playerAnim.ToggleRoll();
    }

    public void MovePlayer(float distance)
    {
        transform.position = new Vector3(transform.position.x + distance, transform.position.y, transform.position.z);
    }

    public void MovePlayerPosition(float x)
    {
        transform.position = new Vector3(x, transform.position.y, transform.position.z);
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (!isInvulnerable && collision.gameObject.tag == "Obstacle")
        {
            collisionCount++;

            if (collisionCount >= 3)
            {
                PlayerDeath?.Invoke();
                isPlayerDead = true;
                sparkEffect.Stop();
            }
            else
            {
                // Start invulnerability and set a timer to end it
                isInvulnerable = true;
                Invoke("EndInvulnerability", invulnerabilityDuration);
            }
        }
    }

    private void EndInvulnerability()
    {
        isInvulnerable = false;
    }



    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Path"))
        {
            sparkEffect.Play();
            scoreText.ticks = 0;
            
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Path"))
        {
            sparkEffect.Stop();

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "WinTrigger")
        {
            PlayerWin?.Invoke();
        }

    }
}

using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    private bool playerStart = false;

    [Header("Player Settings")]
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


    // Start is called before the first frame update
    void Start()
    {
        playerSpeed = LevelSettings.Instance.beatsPerMinute / bpmMultiplier;
  

        playerAnim = GetComponent<PlayerAnimation>();

        GameManager.GameStart += StartPlayer;

        Debug.Log(jumpDistance * AudioManager.Instance.GetSecondsPerBeat());
    }

    // Update is called once per frame
    void Update()
    {
        if (!playerStart || isPlayerDead)
        {
            return;
        }

        if (SwipeManager.swipeRight && !isLaneChanging)
        {
            Debug.Log("Swipe Right");
            startXPos = transform.position.x;
            endXPos = transform.position.x + laneDistance;
            isLaneChanging = true;
         
        }
        else if (SwipeManager.swipeLeft && !isLaneChanging)
        {
            Debug.Log("Swipe Right");
            startXPos = transform.position.x;
            endXPos = transform.position.x - laneDistance;
            isLaneChanging = true;
        
        }
        else if (SwipeManager.swipeUp)
        {
            Debug.Log("Jump");
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

        //playerModel.transform.Rotate(new Vector3(playerSpeed * Time.deltaTime * rotationSpeedMultiplier, 0, 0));

       
    }

    private void StartPlayer()
    {
        playerStart = true;
    }

    IEnumerator Jump(float duration)
    {
        playerAnim.ToggleRoll();
        

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


    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Obstacle")
        {
            PlayerDeath?.Invoke();
            isPlayerDead = true;
            sparkEffect.Stop();

        }
    }



    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Path"))
        {
            sparkEffect.Play();
            Debug.Log("Add Score");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Path"))
        {
            sparkEffect.Stop();
        }
    }


    public void BacktoMainMenu() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


    
}

using System.Collections;
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

    [Header("Jump Settings")]
    [SerializeField] private AnimationCurve jumpCurve;
    [SerializeField] private int jumpDistance = 3;
    private float jumpDistanceInSeconds;
    private float jumpTime;


    [Space(10)]
    //lerp stuff
    [SerializeField] private float laneDistance = 1.8f;
    private float elapsedTime = 0;
    private float laneChangeDuration = 0.03f;
    private bool isLaneChanging = false;
    private float startXPos;
    private float endXPos;

    //UIPanels
    public static bool isCollided = false;
    public GameObject gameoverPanel;

    // Start is called before the first frame update
    void Start()
    {
      gameoverPanel.SetActive(false);   
        playerSpeed = LevelSettings.Instance.beatsPerMinute / bpmMultiplier;
        jumpDistanceInSeconds = jumpDistance * AudioManager.Instance.GetSecondsPerBeat();
        GameManager.GameStart += StartPlayer;
    }

    // Update is called once per frame
    void Update()
    {
        if (!playerStart)
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
            StartCoroutine(Jump(jumpDistanceInSeconds));
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

        transform.Rotate(new Vector3(playerSpeed * Time.deltaTime * rotationSpeedMultiplier, 0, 0));

       
    }

    private void StartPlayer()
    {
        playerStart = true;
    }

    IEnumerator Jump(float duration)
    {
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
    }


    /*
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Obstacle")
        {
            PlayerManager.isCollided = true;
            Debug.Log("TUMAMA");
            Time.timeScale = 0;
            GameOverPanel.SetActive(true);
        }
    }
    */


    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Path"))
        {
            Debug.Log("Add Score");
        }
    }
    

   


    
}

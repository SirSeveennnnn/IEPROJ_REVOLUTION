using System;
using System.Collections;
using UnityEngine;


[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(PlayerStatus))]
[RequireComponent(typeof(PlayerAnimation))]
public class PlayerManager : MonoBehaviour
{
    // temp gameover panels
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject winPanel;

    // render
    [SerializeField] private CameraFollow playerCamera;
    [SerializeField] private Renderer[] modelRendererList;

    // effects and other ui
    [SerializeField] private ParticleSystem sparkEffect;
    [SerializeField] private ScoreText scoreText;

    [SerializeField] private bool isInvulnerable;

    private Collider col;
    private PlayerMovement playerMovement;
    private PlayerStatus playerStatus;
    private PlayerAnimation playerAnimation;

    public event Action OnPlayerDeathEvent;
    public event Action OnPlayerWinEvent;


    private void Start()
    {
        gameOverPanel.SetActive(false);
        winPanel.SetActive(false);

        col = GetComponent<Collider>();
        playerMovement = GetComponent<PlayerMovement>();
        playerStatus = GetComponent<PlayerStatus>();
        playerAnimation = GetComponent<PlayerAnimation>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            KillPlayer();
        }
        else if (other.CompareTag("WinTrigger"))
        {
            OnPlayerWinEvent?.Invoke();

            Time.timeScale = 0.0f;
            //winPanel.SetActive(true);
            sparkEffect.Stop();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Path"))
        {
            sparkEffect.Play();
            //scoreText.ticks = 0;
            //Debug.Log("Add Score");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Path"))
        {
            sparkEffect.Stop();
        }
    }

    public void KillPlayer()
    {
        Debug.Log("dead");

        if (isInvulnerable)
        {
            return;
        }

        OnPlayerDeathEvent?.Invoke();

        Time.timeScale = 0.0f;
        //gameOverPanel.SetActive(true);
        sparkEffect.Stop();
    }

    public Renderer[] GetModelRenderer()
    {
        return modelRendererList;
    }

    #region IFrames
    public void ApplyIFrames(float duration)
    {
        if (duration > 1.75f)
        {
            return;
        }

        StartCoroutine(ApplyIFramesCoroutine(duration));
    }

    private IEnumerator ApplyIFramesCoroutine(float duration)
    {
        col.enabled = false;

        yield return new WaitForSeconds(duration);

        col.enabled = true;
    }
    #endregion

    #region ShrinkPlayer
    public void OnShrukenDown(bool isShrunk)
    {
        Vector2 offset = Vector2.zero;

        if (isShrunk)
        {
            // player
            transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
            transform.position = new Vector3(transform.position.x, 0.65f, transform.position.z);
            playerMovement.UpdateDefaultYPos(0.65f);

            // camera
            Camera cameraComponent = playerCamera.GetComponent<Camera>();

            offset = new Vector2(4.5f, -2.9f);
            playerCamera.ChangeOffset(offset);
            cameraComponent.fieldOfView = 55;
            StartCoroutine(ChangeCameraOrientationCoroutine(offset, 35f));

            //offset = new Vector2(1.8f, -2.8f);
            //StartCoroutine(ChangeCameraOrientationCoroutine(offset, 0f));
        }
        else
        {
            // player
            transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
            transform.position = new Vector3(transform.position.x, 0.85f, transform.position.z);
            playerMovement.UpdateDefaultYPos(0.85f);

            // camera
            Camera cameraComponent = playerCamera.GetComponent<Camera>();

            offset = new Vector2(3.775f, -4.55f);
            playerCamera.ChangeOffset(offset);
            cameraComponent.fieldOfView = 60;
            StartCoroutine(ChangeCameraOrientationCoroutine(offset, 9f));
        }
    }

    private IEnumerator ChangeCameraOrientationCoroutine(Vector2 offset, float rotation)
    {
        Transform cameraTransform = playerCamera.GetComponent<Transform>();

        Vector3 startPos = cameraTransform.position;
        Vector3 targetPos;

        Quaternion startRot = cameraTransform.rotation;
        Quaternion targetRot = Quaternion.Euler(new Vector3(rotation, 0, 0));

        float elapsed = 0f;
        float duration = 0.3f;

        while (elapsed < duration)
        {
            targetPos = new Vector3(cameraTransform.position.x, offset.x, offset.y + transform.position.z);
            cameraTransform.position = Vector3.Lerp(startPos, targetPos, elapsed / duration);
            cameraTransform.rotation = Quaternion.Lerp(startRot, targetRot, elapsed / duration);
            elapsed += Time.deltaTime;

            yield return Time.deltaTime;
        }

        cameraTransform.position = new Vector3(cameraTransform.position.x, offset.x, offset.y + transform.position.z);
        cameraTransform.rotation = Quaternion.Euler(new Vector3(rotation, 0, 0));
    }
    #endregion
}

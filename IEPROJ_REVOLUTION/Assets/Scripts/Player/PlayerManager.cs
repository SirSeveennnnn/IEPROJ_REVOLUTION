using System;
using System.Collections;
using UnityEngine;


[RequireComponent(typeof(Renderer))]
[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(PlayerStatus))]
[RequireComponent(typeof(PlayerAnimation))]
public class PlayerManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject winPanel;
    [SerializeField] private ParticleSystem sparkEffect;
    [SerializeField] private ScoreText scoreText;

    private Renderer r;
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

        r = GetComponent<Renderer>();
        col = GetComponent<Collider>();
        playerMovement = GetComponent<PlayerMovement>();
        playerStatus = GetComponent<PlayerStatus>();
        playerAnimation = GetComponent<PlayerAnimation>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            OnPlayerDeathEvent?.Invoke();

            Time.timeScale = 0.0f;
            gameOverPanel.SetActive(true);

            //sparkEffect.Stop();
        }
        else if (other.CompareTag("WinTrigger"))
        {
            OnPlayerWinEvent?.Invoke();

            Time.timeScale = 0.0f;
            winPanel.SetActive(true);
            //sparkEffect.Stop();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Path"))
        {
            sparkEffect.Play();
            //scoreText.ticks = 0;
            Debug.Log("Add Score");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Path"))
        {
            //sparkEffect.Stop();
        }
    }

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
        float elapsed = 0.0f;
        col.enabled = false;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            yield return Time.deltaTime;
        }

        col.enabled = true;
    }
}

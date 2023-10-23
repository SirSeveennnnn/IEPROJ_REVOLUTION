using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private bool isGameStarted = false;
    public static event Action GameStart;

    [SerializeField] private PlayerManager player;

    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject winPanel;

    public bool IsGameStarted
    {
        get { return isGameStarted; }
        private set { isGameStarted = value; }
    }

    public PlayerManager Player
    {
        get { return player; }
        private set { player = value; }
    }

    void Start()
    {
        player.OnPlayerDeathEvent += OpenGameOverPanel;
        player.OnPlayerWinEvent += OpenWinPanel;
        isGameStarted = false;
    }

    private void OnDestroy()
    {
        player.OnPlayerDeathEvent -= OpenGameOverPanel;
        player.OnPlayerWinEvent -= OpenWinPanel;
    }

    void Update()
    {
        if (SwipeManager.tap && !isGameStarted)
        {
            isGameStarted = true;
            GameStart?.Invoke();
        }
    }

    public void StartGame()
    {
        isGameStarted = true;
        GameStart?.Invoke();
    }

    public void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void OpenGameOverPanel()
    {
        gameOverPanel.SetActive(true);
    }
    private void OpenWinPanel()
    {
        winPanel.SetActive(true);
    }
}

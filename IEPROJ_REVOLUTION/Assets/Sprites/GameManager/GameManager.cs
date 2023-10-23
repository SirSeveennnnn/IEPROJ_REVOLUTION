using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void OnDestroy()
    {
        if (Instance != null && Instance == this)
        {
            player.OnPlayerDeathEvent -= OpenGameOverPanel;
            player.OnPlayerWinEvent -= OpenWinPanel;
            Destroy(this.gameObject);
        }
    }
    #endregion

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

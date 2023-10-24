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

            GestureManager.Instance.OnTapEvent -= OnTap;

            Destroy(this.gameObject);
        }
    }
    #endregion

    private bool isGameStarted = false;
    public static event Action GameStartEvent;

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

        GestureManager.Instance.OnTapEvent += OnTap;

        isGameStarted = false;
    }

    //void Update()
    //{
    //    if (SwipeManager.tap && !isGameStarted)
    //    {
    //        isGameStarted = true;
    //        GameStartEvent?.Invoke();
    //    }
    //}

    private void OnTap(object send, TapEventArgs args)
    {
        if (isGameStarted)
        {
            return;
        }

        isGameStarted = true;
        GameStartEvent?.Invoke();
    }

    public void StartGame()
    {
        isGameStarted = true;
        GameStartEvent?.Invoke();
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

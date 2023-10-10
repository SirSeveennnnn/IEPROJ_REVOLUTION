using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
    private bool isGameStarted = false;
    public static event Action GameStart;

    [SerializeField] private GameObject playerObj;

    [SerializeField] private GameObject gameOverPanel;


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }


    public bool IsGameStarted
    {
        get { return isGameStarted; }
        private set { isGameStarted = value; }
    }

    public GameObject Player
    {
        get { return playerObj; }
        private set { playerObj = value; }
    }

    void Start()
    {
        PlayerMovement.PlayerDeath += OpenPanel;
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

    private void OpenPanel()
    {
        gameOverPanel.SetActive(true);
    }
}

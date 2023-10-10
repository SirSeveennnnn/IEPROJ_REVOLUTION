using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
    private bool isGameStarted = false;
    public static event Action GameStart;

    [SerializeField] private GameObject playerObj;


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
        isGameStarted = false;
    }

    void Update()
    {
        if (SwipeManager.tap && !isGameStarted)
        {
            isGameStarted = true;
            GameStart?.Invoke();
            //audioManager.PlayMusic();
            //Destroy(startingText);
        }
    }

    public void StartGame()
    {
        isGameStarted = true;
        GameStart?.Invoke();
    }
}

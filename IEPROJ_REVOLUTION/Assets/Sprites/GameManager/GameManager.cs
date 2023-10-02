using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    private bool isGameStarted = false;

    public static event Action GameStart;

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

    // Start is called before the first frame update
    void Start()
    {
        isGameStarted = false;
    }

    // Update is called once per frame
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static bool isCollided;
    public GameObject gameOverPanel;
    // Start is called before the first frame update
    void Start()
    {
        PlayerMovement.PlayerDeath += playerDead;
        gameOverPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
    }

    void playerDead() 
    {
        gameOverPanel.SetActive(true);
        Time.timeScale= 0.0f;
    }

}

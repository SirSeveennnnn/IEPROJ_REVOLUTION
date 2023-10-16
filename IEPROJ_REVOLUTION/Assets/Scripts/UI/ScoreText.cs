using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class ScoreText : MonoBehaviour
{
    public TMP_Text Score;
    public float scoreAmount;
    public float pointIncreasePerSecond;
    public float scoreMultiplier;
    private bool isDead;
    // Start is called before the first frame update
    void Start()
    {
        PlayerMovement.PlayerDeath += playerIsDead;
        isDead = false;

        scoreAmount = 0f;
        pointIncreasePerSecond= 1f;
        scoreMultiplier = 1f; 
    }

    // Update is called once per frame
    void Update()
    {
        Score.text = " " +(int)scoreAmount + " " ;
        scoreAmount += pointIncreasePerSecond * scoreMultiplier* Time.deltaTime;
        if(isDead)
        {
            scoreMultiplier = 0;
            pointIncreasePerSecond = 0;
            scoreAmount += pointIncreasePerSecond * scoreMultiplier;
        }
    }
    void playerIsDead() 
    {
        isDead= true;
    }
}

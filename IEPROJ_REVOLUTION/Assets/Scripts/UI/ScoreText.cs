using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class ScoreText : MonoBehaviour
{
    public TMP_Text Score;
    public TMP_Text Multiplier;

    public float scoreAmount;
    public float pointIncreasePerSecond;
    public float scoreMultiplier;
    private bool isDead;


    public float ticks = 0.0f;
    public float duration = 1.0f;
    public float scoreTicks = 0.0f;
    public float scoreDuration = 3.0f;


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
        ticks += Time.deltaTime;


        if (ticks >= duration) {
            scoreMultiplier = 1;
            Debug.Log("MULTIPLIER");
        }
        else
        {
            scoreTicks += Time.deltaTime;
            if (scoreTicks >= scoreDuration)
            {
                scoreMultiplier++;
                scoreTicks= 0;
            }
        }

        Multiplier.text = (int)scoreMultiplier + "x";
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

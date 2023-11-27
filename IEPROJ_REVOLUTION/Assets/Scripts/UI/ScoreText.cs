using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


public class ScoreText : MonoBehaviour
{
    public TMP_Text Score;
    public TMP_Text Multiplier;
    public TMP_Text HighScore1;
    public TMP_Text HighScore2;



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

        HighScore1.text = "High Score: " + PlayerPrefs.GetInt("HighScoreLevel1",0).ToString();
        HighScore2.text = "High Score: " + PlayerPrefs.GetInt("HighScoreLevel2", 0).ToString();


    }

    // Update is called once per frame
    void Update()
    {
       
        ticks += Time.deltaTime;


        if (ticks >= duration) {
            scoreMultiplier = 1;
            //Debug.Log("MULTIPLIER");
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


        Scene scene = SceneManager.GetActiveScene();

        if (scene.name == "ADScene" || scoreAmount > PlayerPrefs.GetInt("HighScoreLevel1",0))
        {
            PlayerPrefs.SetInt("HighScoreLevel1", (int)scoreAmount);
            HighScore1.text = scoreAmount.ToString();
        }
        if (scene.name == "ADScene2" || scoreAmount > PlayerPrefs.GetInt("HighScoreLevel2", 0))
        {
            PlayerPrefs.SetInt("HighScoreLevel2", (int)scoreAmount);
            HighScore1.text = scoreAmount.ToString();
        }
    }
    void playerIsDead() 
    {
        isDead= true;
    }
}

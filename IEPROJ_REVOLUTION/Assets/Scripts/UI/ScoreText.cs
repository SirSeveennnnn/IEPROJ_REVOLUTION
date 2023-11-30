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
    public TMP_Text HighScore3;
    public TMP_Text HighScore4;
    public TMP_Text HighScore5;

    public GameObject Wow;
    public GameObject Cool;
    public GameObject Awesome;
    public GameObject Great;
    public GameObject Super;





    public float scoreAmount;
    public float pointIncreasePerSecond;
    public float scoreMultiplier;
    private bool isDead;


    public float ticks = 0.0f;
    public float duration = 0.5f;
    public float scoreTicks = 0.0f;
    public float scoreDuration = 3.0f;

    public PlayerMovement playerMovement;

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
        HighScore3.text = "High Score: " + PlayerPrefs.GetInt("HighScoreLevel3", 0).ToString();
        HighScore4.text = "High Score: " + PlayerPrefs.GetInt("HighScoreLevel4", 0).ToString();
        HighScore5.text = "High Score: " + PlayerPrefs.GetInt("HighScoreLevel5", 0).ToString();

        Wow.SetActive(false);
        Cool.SetActive(false);
        Awesome.SetActive(false);
        Great.SetActive(false);
        Super.SetActive(false);



    }

    // Update is called once per frame
    void Update()
    {
       
        ticks += Time.deltaTime;

       
        if (ticks >= duration ) {
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
        //Overlay Effects

        //Wow
        if(scoreMultiplier >= 4 && scoreMultiplier <= 7)
        {
            Wow.SetActive(true);
        }
        else
        {
            Wow.SetActive(false);
        }

        //Cool
        if (scoreMultiplier >= 8 && scoreMultiplier <= 11)
        {
            Cool.SetActive(true);
        }
        else
        {
            Cool.SetActive(false);
        }

        //Awesome
        if (scoreMultiplier >= 12 && scoreMultiplier <= 15)
        {
            Awesome.SetActive(true);
        }
        else
        {
            Awesome.SetActive(false);
        }

        //Great
        if (scoreMultiplier >= 16 && scoreMultiplier <= 20)
        {
            Great.SetActive(true);
        }
        else
        {
            Great.SetActive(false);
        }

        //Super
        if (scoreMultiplier >= 21)
        {
            Super.SetActive(true);
        }
        else
        {
            Super.SetActive(false);
        }






        //High Score setting
        Scene scene = SceneManager.GetActiveScene();

        if (scene.name == "ADScene" && scoreAmount > PlayerPrefs.GetInt("HighScoreLevel1",0))
        {
            PlayerPrefs.SetInt("HighScoreLevel1", (int)scoreAmount);
            HighScore1.text = scoreAmount.ToString();
        }
        if (scene.name == "ADScene2" && scoreAmount > PlayerPrefs.GetInt("HighScoreLevel2", 0))
        {
            PlayerPrefs.SetInt("HighScoreLevel2", (int)scoreAmount);
            HighScore1.text = scoreAmount.ToString();
        }
        if (scene.name == "ADScene3" && scoreAmount > PlayerPrefs.GetInt("HighScoreLevel3", 0))
        {
            PlayerPrefs.SetInt("HighScoreLevel3", (int)scoreAmount);
            HighScore3.text = scoreAmount.ToString();
        }
        if (scene.name == "ADScene4" && scoreAmount > PlayerPrefs.GetInt("HighScoreLevel4", 0))
        {
            PlayerPrefs.SetInt("HighScoreLevel4", (int)scoreAmount);
            HighScore4.text = scoreAmount.ToString();
        }
        if (scene.name == "ADScene5" && scoreAmount > PlayerPrefs.GetInt("HighScoreLevel5", 0))
        {
            PlayerPrefs.SetInt("HighScoreLevel5", (int)scoreAmount);
            HighScore5.text = scoreAmount.ToString();
        }
    }
    void playerIsDead() 
    {
        isDead= true;
    }
}

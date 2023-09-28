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
    // Start is called before the first frame update
    void Start()
    {
        scoreAmount = 0f;
        pointIncreasePerSecond= 1f;
    }

    // Update is called once per frame
    void Update()
    {
        Score.text = "Score: " + (int)scoreAmount ;
        scoreAmount += pointIncreasePerSecond * Time.deltaTime;
    }
}

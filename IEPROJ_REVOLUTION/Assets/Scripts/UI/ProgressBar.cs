using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    //public Slider progressBar;
    public bool isGameStart = false;
    public Image img;

    private void Start()
    {
        img = GetComponent<Image>();
        GameManager.GameStart += gamestart;
    }

    private void Update()
    {
        //if (isGameStart)    
       // progressBar.value = AudioManager.Instance.songPositionInBeats / AudioManager.Instance.totalBeats;

        if (isGameStart)
        img.fillAmount= AudioManager.Instance.songPositionInBeats / AudioManager.Instance.totalBeats;
    }
    private void gamestart()
    {
            isGameStart= true;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
   
    public void LoadGameScene()
    {
        // Load the "GameScene" when the button is pressed
        SceneManager.LoadScene("MOKKA V2");
    }
    public void TutorialScene()
    {
        // Load the "GameScene" when the button is pressed
        SceneManager.LoadScene("TutorialScene");
    }
    public void Quit()
    {
       Application.Quit();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{

    private void Update()
    {
        
    }
    public void LoadTutorialScene()
    {
        // Load the "GameScene" when the button is pressed
        SceneManager.LoadScene("TutorialScene");
    }
    public void LoadGameScene()
    {
        // Load the "GameScene" when the button is pressed
        SceneManager.LoadScene("SpiralsScene");
    }
    public void MainMenu() 
    {
        SceneManager.LoadScene("UI");
    }
    public void Quit()
    {
       Application.Quit();
    }
}

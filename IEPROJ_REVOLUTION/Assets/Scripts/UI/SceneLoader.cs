using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
   
    public void LoadGameScene()
    {
        // Load the "GameScene" when the button is pressed
        SceneManager.LoadScene("MOKKA V2", LoadSceneMode.Single);
    }
    public void LoadLevel1()
    {
        // Load the "GameScene" when the button is pressed
        SceneManager.LoadScene("ADScene", LoadSceneMode.Single);
    }
    public void LoadLevel2()
    {
        // Load the "GameScene" when the button is pressed
        SceneManager.LoadScene("ADScene2", LoadSceneMode.Single);
    }
    public void LoadLevel3()
    {
        // Load the "GameScene" when the button is pressed
        SceneManager.LoadScene("ADScene3", LoadSceneMode.Single);
    }
    public void LoadLevel4()
    {
        // Load the "GameScene" when the button is pressed
        SceneManager.LoadScene("ADScene4", LoadSceneMode.Single);
    }
    public void TutorialScene()
    {
        // Load the "GameScene" when the button is pressed
        SceneManager.LoadScene("TutorialScene");
    }
    public void MainMenuScene()
    {
        // Load the "GameScene" when the button is pressed
        SceneManager.LoadScene("TempMainMenu");
    }
    public void Quit()
    {
       Application.Quit();
    }
}

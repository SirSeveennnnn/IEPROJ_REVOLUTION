using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTransition : MonoBehaviour
{

    public Camera StartScreenCamera;
    public Camera MainMenuCamera;
    public Camera OptionsCamera;


    public void Start()
    {
        StartScreenCamera.enabled = true;
        MainMenuCamera.enabled = false;
        OptionsCamera.enabled = false;
    }
    public void LoadLevelSelect()
    {
        StartScreenCamera.enabled = false;
        MainMenuCamera.enabled = true;
        OptionsCamera.enabled = false;
    }
    public void LoadOptions()
    {
        StartScreenCamera.enabled = false;
        MainMenuCamera.enabled = false;
        OptionsCamera.enabled = true;
    }
    public void LoadStartScreen()
    {
        StartScreenCamera.enabled = true;
        MainMenuCamera.enabled = false;
        OptionsCamera.enabled = false;
    }

}

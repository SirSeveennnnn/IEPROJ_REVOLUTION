using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialHandler : MonoBehaviour
{

    public GameObject[] windows;
    private int currentWindow = 0;

    private enum windowValues
    {
        welcomeWindow = 0,
        tapLeftWindow = 1,
        tapRightWindow = 2,
        jumpWindow = 3,
        neonPathWindow = 4,
        forceLeftWindow = 5,
        forceRightWindow = 6,
        portalWindow = 7,
        gateWindow = 8
    };

    private void Start()
    {
        foreach (GameObject go in windows)
        {
            go.SetActive(false);
        }
    }

    public void OpenTutorialWindow()
    {
        windows[currentWindow].SetActive(true);
        Time.timeScale = 0;
        AudioManager.Instance.PauseMusic();

    }

    public void ProceedTutorial()
    {
        windows[currentWindow].SetActive(false);
        Time.timeScale = 1;
        AudioManager.Instance.PlayMusic();

        currentWindow++;
    }
}

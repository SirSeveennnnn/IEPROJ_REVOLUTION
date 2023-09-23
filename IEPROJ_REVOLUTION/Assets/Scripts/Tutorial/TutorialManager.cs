using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private List<TutorialTrigger> triggersList = new();

    private void Start()
    {
        SwipeManager.isEnabled = false;
    }

    private void Update()
    {
        if (!GameManager.Instance.IsGameStarted)
        {
            GameManager.Instance.StartGame();
        }

        if (triggersList.Count == 0 || !triggersList[0].IsTriggered)
        {
            return;
        }

        Time.timeScale = 0;

        if (triggersList[0].GestureType == EGestureTypes.Tap && SwipeManager.tap ||
            triggersList[0].GestureType == EGestureTypes.SwipeLeft && SwipeManager.swipeLeft ||
            triggersList[0].GestureType == EGestureTypes.SwipeRight && SwipeManager.swipeRight)
        {
            Time.timeScale = 1;
            SwipeManager.isEnabled = false;

            triggersList[0].OnCorrectGesture();
            triggersList.RemoveAt(0);
        }
    }
}

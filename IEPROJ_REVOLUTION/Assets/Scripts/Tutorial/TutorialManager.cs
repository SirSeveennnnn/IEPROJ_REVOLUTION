using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private PlayerMovement movementScript;
    [SerializeField] private List<TutorialTrigger> triggersList = new();


    private void Start()
    {
        GestureManager.Instance.OnTapEvent += OnTap;
        GestureManager.Instance.OnSwipeEvent += OnSwipe;

        GestureManager.Instance.OnEnableInputs(false);
        //SwipeManager.isEnabled = false;
    }

    private void OnDisable()
    {
        GestureManager.Instance.OnTapEvent -= OnTap;
        GestureManager.Instance.OnSwipeEvent -= OnSwipe;
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
        //movementScript.enabled = false;

        //if (triggersList[0].GestureType == EGestureTypes.Tap && SwipeManager.tap ||
        //    triggersList[0].GestureType == EGestureTypes.SwipeLeft && SwipeManager.swipeLeft ||
        //    triggersList[0].GestureType == EGestureTypes.SwipeRight && SwipeManager.swipeRight)
        //{
        //    Time.timeScale = 1;
        //    SwipeManager.isEnabled = false;

        //    triggersList[0].OnCorrectGesture();
        //    triggersList.RemoveAt(0);
        //}
    }

    private void OnTap(object send, TapEventArgs args)
    {
        if (triggersList[0].GestureType == EGestureTypes.Tap)
        {
            Time.timeScale = 1;
            //movementScript.enabled = true;
            GestureManager.Instance.OnEnableInputs(false);

            triggersList[0].OnCorrectGesture();
            triggersList.RemoveAt(0);
        }
    }

    private void OnSwipe(object send, SwipeEventArgs args)
    {
        Debug.Log(args.SwipeDirection.ToString());

        if (triggersList[0].GestureType == EGestureTypes.SwipeLeft && args.SwipeDirection == SwipeEventArgs.SwipeDirections.LEFT ||
            triggersList[0].GestureType == EGestureTypes.SwipeRight && args.SwipeDirection == SwipeEventArgs.SwipeDirections.RIGHT)
        {
            Time.timeScale = 1;
            //movementScript.enabled = true;
            GestureManager.Instance.OnEnableInputs(false);

            triggersList[0].OnCorrectGesture();
            triggersList.RemoveAt(0);
        }
    }
}

using UnityEngine;

public class TutorialInputTrigger : TutorialTrigger
{
    [SerializeField] private EGestureTypes gestureToCheck;
    [SerializeField] private RectTransform tapPanelTransform;
    [SerializeField] private string afterGestureText = "";

    private PlayerMovement movementScript;


    private void Start()
    {
        movementScript = GameManager.Instance.Player.GetComponent<PlayerMovement>();

        GestureManager.Instance.OnTapEvent += OnTap;
        GestureManager.Instance.OnSwipeEvent += OnSwipe;
    }

    private void OnDisable()
    {
        GestureManager.Instance.OnTapEvent -= OnTap;
        GestureManager.Instance.OnSwipeEvent -= OnSwipe;
    }

    private void OnTap(object send, TapEventArgs args)
    {
        if (TutorialManager.Instance.GetCurrentTrigger() != this)
        {
            return;
        }

        if (gestureToCheck == EGestureTypes.Tap)
        {
            if (tapPanelTransform != null && !RectTransformUtility.RectangleContainsScreenPoint(tapPanelTransform, args.TapPosition))
            {
                return;
            }

            OnResume();
        }
    }

    private void OnSwipe(object send, SwipeEventArgs args)
    {
        if (TutorialManager.Instance.GetCurrentTrigger() != this)
        {
            return;
        }

        if (gestureToCheck == EGestureTypes.SwipeLeft && args.SwipeDirection == SwipeEventArgs.SwipeDirections.LEFT ||
            gestureToCheck == EGestureTypes.SwipeRight && args.SwipeDirection == SwipeEventArgs.SwipeDirections.RIGHT || 
            gestureToCheck == EGestureTypes.SwipeUp && args.SwipeDirection == SwipeEventArgs.SwipeDirections.UP || 
            gestureToCheck == EGestureTypes.SwipeDown && args.SwipeDirection == SwipeEventArgs.SwipeDirections.DOWN)
        {
            OnResume();
        }
    }

    private void OnResume()
    {
        Time.timeScale = 1.0f;
        movementScript.disableMovement = false;

        if (afterGestureText != "")
        {
            TutorialManager.Instance.UpdateMainText(this, afterGestureText);
        }
        else
        {
            TutorialManager.Instance.DisableText();
        }

        GestureManager.Instance.enabled = false;
    }

    protected override void OnTutorialTrigger()
    {
        base.OnTutorialTrigger();

        GestureManager.Instance.enabled = true;
        Time.timeScale = 0.0f;

        movementScript.disableMovement = true;
    }
}

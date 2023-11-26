using UnityEngine;

public class SwipeManager : MonoBehaviour
{
    public static bool tap, tapLeft, tapRight;
    public static bool swipeLeft, swipeRight, swipeUp, swipeDown;
    public static bool isEnabled = true;

    private bool isDraging = false;
    [SerializeField] private Vector2 startTouch, endTouch, swipeDelta;

    [SerializeField] private float duration = 0;

    [SerializeField] public float tapTime = 0.05f;

    private void Update()
    {
    

        tap = swipeDown = swipeUp = swipeLeft = swipeRight = tapLeft = tapRight = false;

        if (!isEnabled)
        {
            return;
        }    

    
        

        if (Input.touchCount > 0)
        {
            if (Input.touches[0].phase == TouchPhase.Began)
            {
                startTouch = Input.touches[0].position;
            }
            else if (Input.touches[0].phase == TouchPhase.Moved)
            {
                duration += Time.deltaTime;
            }
            else if (Input.touches[0].phase == TouchPhase.Ended)
            {
                endTouch = Input.touches[0].position;

                if (duration < tapTime)
                {
                    tap = true;
                    if (endTouch.x <= (Screen.currentResolution.width / 2))
                    {
                        Debug.Log("Tap Left");
                        tapLeft = true;
                    }
                    else if (endTouch.x > (Screen.currentResolution.width / 2))
                    {
                        Debug.Log("Tap Right");
                        tapRight = true;
                    }
                }
                else if (duration > tapTime)
                {
                    
                    swipeDelta = endTouch - startTouch;
                    float x = swipeDelta.x;
                    float y = swipeDelta.y;
                    if (Mathf.Abs(x) > Mathf.Abs(y))
                    {
                        //Left or Right
                        if (x < 0)
                            swipeLeft = true;
                        else
                            swipeRight = true;
                    }
                    else
                    {
                        //Up or Down
                        if (y < 0)
                            swipeDown = true;
                        else
                        {
                            Debug.Log("Swipe Up");
                            swipeUp = true;
                        }
                            
                    }

                }

                duration = 0;
                startTouch = endTouch = swipeDelta = Vector2.zero;
                Reset();
            }
        }

    }

    private void Reset()
    {
        startTouch = swipeDelta = Vector2.zero;
        isDraging = false;
    }
}
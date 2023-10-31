using UnityEngine;
using UnityEngine.UI;

public class QuestUI : MonoBehaviour
{
    [SerializeField] private GameObject questPanel;
    [SerializeField] private GameObject swipeUpPrefab;
    [SerializeField] private GameObject swipeLeftPrefab;
    [SerializeField] private GameObject swipeRightPrefab;

    [SerializeField] private Slider countdownSlider;
    [SerializeField] private Image fillImage;
    [SerializeField] private Gradient fillGradient;

    private void Start()
    {
        countdownSlider.value = 1;
    }

    public Image AddGestureToQuest(EGestureTypes gesture)
    {
        Image gestureImageRef = null;

        if (gesture == EGestureTypes.SwipeLeft)
        {
            GameObject newObj = GameObject.Instantiate(swipeLeftPrefab, questPanel.transform);
            gestureImageRef = newObj.GetComponent<Image>();
        }
        else if (gesture == EGestureTypes.SwipeRight)
        {
            GameObject newObj = GameObject.Instantiate(swipeRightPrefab, questPanel.transform);
            gestureImageRef = newObj.GetComponent<Image>();
        }
        else if (gesture == EGestureTypes.SwipeUp)
        {
            GameObject newObj = GameObject.Instantiate(swipeUpPrefab, questPanel.transform);
            gestureImageRef = newObj.GetComponent<Image>();
        }

        return gestureImageRef;
    }

    public void UpdateSlider(float value)
    {
        countdownSlider.value = 1f - value;
        
        fillImage.color = fillGradient.Evaluate(value);
    }
}

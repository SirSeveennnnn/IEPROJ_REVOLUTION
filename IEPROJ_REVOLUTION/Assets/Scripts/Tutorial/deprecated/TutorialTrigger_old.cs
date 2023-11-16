using UnityEngine;

public class TutorialTrigger_old : MonoBehaviour
{
    [SerializeField] private bool isTriggered = false;
    [SerializeField] private GameObject textDisplay = null;
    [SerializeField] private EGestureTypes gestureType = EGestureTypes.Unknown;


    public bool IsTriggered
    {
        get { return isTriggered; }
        private set { IsTriggered = value; }
    }

    public EGestureTypes GestureType
    {
        get { return gestureType; }
        private set { gestureType = value; }
    }

    public void OnCorrectGesture()
    {
        textDisplay.SetActive(false);
        Destroy(this.gameObject);
    }

    private void Start()
    {
        isTriggered = false;
        textDisplay.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            isTriggered = true;
            textDisplay.SetActive(true);

            GestureManager.Instance.OnEnableInputs(true);
            //SwipeManager.isEnabled = true;
        }
    }
}

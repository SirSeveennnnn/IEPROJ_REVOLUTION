using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class TutorialTrigger : MonoBehaviour
{
    [TextArea(5, 10)]
    [SerializeField] protected string tutorialText;

    private Collider col;
    private Rigidbody rb;


    private void Start()
    {
        tag = "Tutorial";

        col = GetComponent<Collider>();
        rb = GetComponent<Rigidbody>();

        col.isTrigger = true;
        rb.useGravity = false;
        rb.isKinematic = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnTutorialTrigger();
        }
    }

    protected virtual void OnTutorialTrigger()
    {
        TutorialManager.Instance.UpdateMainText(this, tutorialText);
    }
}

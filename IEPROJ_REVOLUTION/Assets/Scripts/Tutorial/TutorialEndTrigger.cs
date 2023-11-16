using System.Collections;
using UnityEngine;

public class TutorialEndTrigger : TutorialTrigger
{
    [SerializeField] private float TutorialEndDuration;

    protected override void OnTutorialTrigger()
    {
        base.OnTutorialTrigger();

        StartCoroutine(TutorialEndCoroutine());
    }

    private IEnumerator TutorialEndCoroutine()
    {
        yield return new WaitForSecondsRealtime(TutorialEndDuration);

        // return to menu
    }
}

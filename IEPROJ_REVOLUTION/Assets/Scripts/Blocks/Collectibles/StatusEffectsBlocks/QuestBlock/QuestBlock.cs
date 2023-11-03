using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestBlock : TimedEffectCollectible
{
    [Space(10)]
    [Header("Quest Properties")]
    [SerializeField] private GameObject questPanel;
    [SerializeField] private GameObject emptyQuestParent;

    [Space(10)]
    [SerializeField] private List<EGestureTypes> gesturesList;
    [SerializeField] private int bonusPoints;
    [SerializeField] private int reductionPoints;

    private int maxQuests;
    private int numCorrectGestures;
    private QuestUI questUI;
    private List<Image> gesturesImageList;


    #region Setup
    private void Start()
    {
        GestureManager.Instance.OnSwipeEvent += OnSwipe;

        maxQuests = 3;
        numCorrectGestures = 0;
        gesturesImageList = new List<Image>();

        CreateGestureImages();
    }

    private void OnDisable()
    {
        GestureManager.Instance.OnSwipeEvent -= OnSwipe;
    }

    private void CreateGestureImages()
    {
        GameObject parent = GameObject.Instantiate(emptyQuestParent, questPanel.transform);
        questUI = parent.GetComponent<QuestUI>();
        parent.name = gameObject.name;

        foreach (EGestureTypes gesture in gesturesList)
        {
            Image newImage = questUI.AddGestureToQuest(gesture);
            gesturesImageList.Add(newImage);
        }

        parent.SetActive(false);
        emptyQuestParent = parent;
    }
    #endregion

    #region Input
    private void OnSwipe(object send, SwipeEventArgs args)
    {
        if (!hasBeenCollected)
        {
            return;
        }

        if (CheckGesture(args.SwipeDirection))
        {
            gesturesImageList[numCorrectGestures].color = Color.green;
            numCorrectGestures++;

            if (numCorrectGestures == gesturesList.Count)
            {
                // add score
                Debug.Log("Quest: add score");

                emptyQuestParent.SetActive(false);
                this.gameObject.SetActive(false);

                StopEffect();
                DisableEffect();
            }
        }
        else
        {
            foreach (Image image in gesturesImageList)
            {
                image.color = Color.white;
            }
            numCorrectGestures = 0;
        }
    }

    private bool CheckGesture(SwipeEventArgs.SwipeDirections inputGesture)
    {
        EGestureTypes currentGestureToCheck = gesturesList[numCorrectGestures];

        bool isMatchingGesture = (inputGesture == SwipeEventArgs.SwipeDirections.LEFT && currentGestureToCheck == EGestureTypes.SwipeLeft);
        isMatchingGesture = isMatchingGesture || (inputGesture == SwipeEventArgs.SwipeDirections.RIGHT && currentGestureToCheck == EGestureTypes.SwipeRight);
        isMatchingGesture = isMatchingGesture || (inputGesture == SwipeEventArgs.SwipeDirections.UP && currentGestureToCheck == EGestureTypes.SwipeUp);

        return isMatchingGesture;
    }
    #endregion

    protected override void OnCollect()
    {
        elapsed = 0;
        playerStatusScript = playerObj.GetComponent<PlayerStatus>();

        List<TimedEffectCollectible> effectsList = playerStatusScript.GetCurrentTimedEffects(effect);

        if (effectsList != null && effectsList.Count > 0)
        {
            OnStackEffect(effectsList);
        }
        else
        {
            emptyQuestParent.SetActive(true);
            StartEffect();
        }
    }

    protected override void OnStackEffect(List<TimedEffectCollectible> effectsList)
    {
        if (effectsList.Count >= maxQuests)
        {
            StopEffect();
            DisableEffect();
        }
        else
        {
            emptyQuestParent.SetActive(true);
            StartEffect();
        }
    }

    protected override IEnumerator TriggerEffect()
    {
        while (elapsed < effectDuration)
        {
            elapsed += Time.deltaTime;
            questUI.UpdateSlider(elapsed / effectDuration);

            yield return Time.deltaTime;
        }

        //yield return new WaitForSeconds(effectDuration);

        // reduce score
        Debug.Log("Quest: reduce score");

        emptyQuestParent.SetActive(false);
        this.gameObject.SetActive(false);
    }
}

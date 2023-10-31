using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestBlock : Collectible
{
    [Space(10)]
    [Header("Quest Properties")]
    [SerializeField] private GameObject questPanel;
    [SerializeField] private GameObject emptyQuestParent;

    [Space(10)]
    [SerializeField] private List<EGestureTypes> gesturesList;
    [SerializeField, Range(9.5f, 45)] private float questDuration;
    [SerializeField] private int bonusPoints;
    [SerializeField] private int reductionPoints;

    private int currentGesture;
    private QuestUI questUI;
    private List<Image> gesturesImageList;
    private Coroutine countdownCoroutine;


    #region Setup
    private void Start()
    {
        GestureManager.Instance.OnSwipeEvent += OnSwipe;

        currentGesture = 0;
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
            gesturesImageList[currentGesture].color = Color.green;
            currentGesture++;

            if (currentGesture == gesturesList.Count)
            {
                StopCoroutine(countdownCoroutine);

                // add score
                Debug.Log("Quest: add score");

                emptyQuestParent.SetActive(false);
                this.gameObject.SetActive(false);
            }
        }
        else
        {
            foreach (Image image in gesturesImageList)
            {
                image.color = Color.white;
            }
            currentGesture = 0;
        }
    }

    private bool CheckGesture(SwipeEventArgs.SwipeDirections inputGesture)
    {
        EGestureTypes currentGestureToCheck = gesturesList[currentGesture];

        bool isMatchingGesture = (inputGesture == SwipeEventArgs.SwipeDirections.LEFT && currentGestureToCheck == EGestureTypes.SwipeLeft);
        isMatchingGesture = isMatchingGesture || (inputGesture == SwipeEventArgs.SwipeDirections.RIGHT && currentGestureToCheck == EGestureTypes.SwipeRight);
        isMatchingGesture = isMatchingGesture || (inputGesture == SwipeEventArgs.SwipeDirections.UP && currentGestureToCheck == EGestureTypes.SwipeUp);

        return isMatchingGesture;
    }
    #endregion

    protected override void OnCollect()
    {
        emptyQuestParent.SetActive(true);
        countdownCoroutine = StartCoroutine(CountDownCoroutine());
    }

    private IEnumerator CountDownCoroutine()
    {
        float elapsed = 0f;

        while (elapsed < questDuration)
        {
            elapsed += Time.deltaTime;
            questUI.UpdateSlider(elapsed / questDuration);

            yield return Time.deltaTime;
        }

        //yield return new WaitForSeconds(questDuration);

        // reduce score
        Debug.Log("Quest: reduce score");

        emptyQuestParent.SetActive(false);
        this.gameObject.SetActive(false);
    }
}

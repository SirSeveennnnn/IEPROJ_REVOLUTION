using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    #region Singleton
    public static TutorialManager Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void OnDestroy()
    {
        if (Instance != null && Instance == this)
        {
            Destroy(this.gameObject);
        }
    }
    #endregion

    [Header("Fading Properties")]
    [SerializeField] private float warmupDuration;
    [SerializeField] private float fadeOutDuration;
    [SerializeField] private Image darkPanel;

    [Header("UI")]
    [SerializeField] private GameObject mainPanel;
    [SerializeField] private GameObject subPanel;
    [SerializeField] private TMP_Text mainText;
    [SerializeField] private TMP_Text subText;

    private TutorialTrigger currentTrigger;

    //private PlayerManager playerScript;
    //private string currentText;


    private void Start()
    {
        //playerScript = GameManager.Instance.Player;
        //playerScript.OnPlayerDeathEvent += OnPlayerDeath;

        GestureManager.Instance.enabled = false;

        warmupDuration = 0.9f;

        darkPanel.gameObject.SetActive(true);
        darkPanel.color = new Color(0f, 0f, 0f, 1f);

        currentTrigger = null;

        StartCoroutine(WarmUpCoroutine());
    }

    //private void OnDisable()
    //{
    //    playerScript.OnPlayerDeathEvent -= OnPlayerDeath;
    //}

    private IEnumerator WarmUpCoroutine()
    {
        yield return new WaitForSeconds(warmupDuration);

        if (!GameManager.Instance.IsGameStarted)
        {
            GameManager.Instance.StartGame();
        }

        StartCoroutine(FadeOutCoroutine());
    }

    private IEnumerator FadeOutCoroutine()
    {
        float elapsed = 0f;

        while (elapsed < fadeOutDuration)
        {
            elapsed += Time.deltaTime;

            float alpha = 1 - Mathf.Pow(elapsed / fadeOutDuration, 3f);
            darkPanel.color = new Color(0f, 0f, 0f, alpha);

            yield return Time.deltaTime;
        }

        darkPanel.color = new Color(0f, 0f, 0f, 0f);
        darkPanel.gameObject.SetActive(false);
    }

    public void UpdateMainText(TutorialTrigger trigger, string text)
    {
        if (currentTrigger != null)
        {
            currentTrigger.enabled = false;
            currentTrigger.gameObject.SetActive(false);
        }

        currentTrigger = trigger;

        //currentText = text;

        mainText.text = text;
        mainPanel.SetActive(true);

        subPanel.SetActive(false);
    }

    public void UpdateSubText(TutorialTrigger trigger, string text)
    {
        if (currentTrigger != null && currentTrigger != trigger)
        {
            currentTrigger.enabled = false;
            currentTrigger.gameObject.SetActive(false);
        }

        currentTrigger = trigger;

        if (!mainText.gameObject.activeSelf)
        {
            return;
        }

        mainText.text = "";
        mainPanel.SetActive(false);

        subText.text = text;
        subPanel.SetActive(true);
    }

    public void DisableText()
    {
        //currentText = "";

        mainText.text = "";
        mainPanel.SetActive(false);

        subText.text = "";
        subPanel.SetActive(false);
    }

    public TutorialTrigger GetCurrentTrigger()
    {
        return currentTrigger;
    }

    //private void OnPlayerDeath()
    //{
    //    if (!mainText.gameObject.activeSelf)
    //    {
    //        return;
    //    }

    //    mainText.text = "";
    //    mainText.gameObject.SetActive(false);

    //    subText.text = currentText;
    //    subText.gameObject.SetActive(true);
    //}
}

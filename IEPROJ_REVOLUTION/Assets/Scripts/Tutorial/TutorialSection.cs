using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialSection : TutorialTrigger
{
    [SerializeField] private List<GameObject> resettableBlocks = new(); // interfaces are not serializable

    private PlayerManager playerScript;
    private PlayerMovement movementScript;
    private float respawnDelay;

    private void Start()
    {
        playerScript = GameManager.Instance.Player;
        playerScript.OnPlayerDeathEvent += OnPlayerDeath;

        movementScript = playerScript.GetComponent<PlayerMovement>();

        respawnDelay = 1.75f;
    }

    private void OnDisable()
    {
        playerScript.OnPlayerDeathEvent -= OnPlayerDeath;
    }

    protected override void OnTutorialTrigger()
    {
        TutorialManager.Instance.UpdateSubText(this, tutorialText);
        GestureManager.Instance.enabled = true;
    }

    private void OnPlayerDeath()
    {
        if (TutorialManager.Instance.GetCurrentTrigger() != this)
        {
            return;
        }

        GestureManager.Instance.enabled = false;

        StartCoroutine(OnPlayerDeathCoroutine());
    }

    private IEnumerator OnPlayerDeathCoroutine()
    {
        yield return new WaitForSecondsRealtime(respawnDelay);

        foreach (GameObject block in resettableBlocks)
        {
            IResettable resetScript = block.GetComponent<IResettable>();
            
            if (resetScript != null)
            {
                resetScript.OnReset();
            }
        }

        Vector2 respawnPoint = new Vector2(transform.position.x, transform.position.z);
        playerScript.RespawnPlayer(respawnPoint);
        movementScript.disableMovement = false;

        Time.timeScale = 1.0f; 
        TutorialManager.Instance.UpdateSubText(this, tutorialText);
    }
}

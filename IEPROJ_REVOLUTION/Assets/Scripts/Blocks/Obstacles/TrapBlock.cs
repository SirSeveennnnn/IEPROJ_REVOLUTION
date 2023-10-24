using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class TrapBlock : MonoBehaviour
{
    [SerializeField] private LevelSettings levelSettings;
    [SerializeField] private int triggerDistanceByBlock;

    [SerializeField] private float blockTravelDistance;
    [SerializeField] private float spikeTravelDistance;
    [SerializeField] private float moveDuration;

    [SerializeField] private GameObject uprightSpike;
    [SerializeField] private GameObject upsideDownSpike;

    private bool isMovementTriggered;

    private GameObject playerObj;
    private Collider col;
    private Rigidbody rb;


    private void Start()
    {
        isMovementTriggered = false;

        tag = "Obstacle";

        playerObj = GameManager.Instance.Player.gameObject;

        col = GetComponent<Collider>();
        rb = GetComponent<Rigidbody>();

        col.isTrigger = true;
        rb.useGravity = false;
        rb.isKinematic = true;
    }

    private void Update()
    {
        if (!isMovementTriggered && (transform.position.z - playerObj.transform.position.z) <= levelSettings.laneDistance * triggerDistanceByBlock)
        {
            isMovementTriggered = true;

            StartCoroutine(MoveSpikeCoroutine(uprightSpike, true));
            StartCoroutine(MoveSpikeCoroutine(upsideDownSpike, false));
            StartCoroutine(MoveBlockCoroutine());
        }
    }

    private IEnumerator MoveBlockCoroutine()
    {
        float elapsed = 0f;
        float startYPos = transform.position.y;
        float targetYPos = startYPos + blockTravelDistance;

        while (elapsed < moveDuration)
        {
            transform.position = new Vector3(transform.position.x, Mathf.Lerp(startYPos, targetYPos, elapsed / moveDuration), transform.position.z);
            elapsed += Time.deltaTime;

            yield return Time.deltaTime;
        }

        transform.position = new Vector3(transform.position.x, targetYPos, transform.position.z);
        this.enabled = false;
    }

    private IEnumerator MoveSpikeCoroutine(GameObject spike, bool isGoingUp)
    {
        float elapsed = 0f;
        float startYPos = spike.transform.localPosition.y;
        float targetYPos = isGoingUp ? startYPos + spikeTravelDistance : startYPos - spikeTravelDistance;

        while (elapsed < moveDuration)
        {
            spike.transform.localPosition = new Vector3(spike.transform.localPosition.x, Mathf.Lerp(startYPos, targetYPos, elapsed / moveDuration), spike.transform.localPosition.z);
            elapsed += Time.deltaTime;

            yield return Time.deltaTime;    
        }

        spike.transform.localPosition = new Vector3(spike.transform.localPosition.x, targetYPos, spike.transform.localPosition.z);
    }

    public void SetLevelSettings(LevelSettings levelSettings)
    {
        this.levelSettings = levelSettings;
    }
}

using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class CannonBlock : MonoBehaviour
{
    [Header("Cannon")]
    [SerializeField] private LevelSettings levelSettings;
    [SerializeField] private int triggerDistanceByBlock = 8;

    [SerializeField] private float jumpHeight = 2.5f;
    [SerializeField] private float jumpDuration = 0.65f;

    [SerializeField] private bool isFakeCannon = false;

    [Header("Bullet Properties")]
    [SerializeField] private BulletBlock bullet;
    [SerializeField] private float bulletSpeed = 4.45f;

    private bool isCannonTriggered;
    private float chargeDuration;
    private GameObject playerObj;
    private Renderer r;


    private void Start()
    {
        if (!isFakeCannon && bullet == null)
        {
            gameObject.SetActive(false);
            Debug.Log("NO BULLETS " +  gameObject.name);
        }

        isCannonTriggered = false;
        chargeDuration = 0.3f;

        playerObj = GameManager.Instance.Player.gameObject;
        r = GetComponent<Renderer>(); 
    }

    private void Update()
    {
        if (!isCannonTriggered && (transform.position.z - playerObj.transform.position.z) <= levelSettings.laneDistance * triggerDistanceByBlock)
        {
            isCannonTriggered = true;

            StartCoroutine(TriggerCannonCoroutine());
        }
    }

    private IEnumerator TriggerCannonCoroutine()
    {
        float elapsed = 0.0f;
        float startYPos = transform.position.y;
        float targetYPos = startYPos + jumpHeight;

        while (elapsed < jumpDuration)
        {
            float x = elapsed / jumpDuration;
            float lerpValue = Mathf.Sqrt(1f - Mathf.Pow(1f - x, 2));
            transform.position = new Vector3(transform.position.x, Mathf.Lerp(startYPos, targetYPos, lerpValue), transform.position.z);

            elapsed += Time.deltaTime;

            yield return Time.deltaTime;
        }

        yield return new WaitForSeconds(chargeDuration);

        if (!isFakeCannon)
        {
            bullet.TriggerBullet(bulletSpeed, playerObj, gameObject);
        }
        
        r.enabled = false;
        this.enabled = false;
    }

    public void SetLevelSettings(LevelSettings levelSettings)
    {
        this.levelSettings = levelSettings;
    }
}

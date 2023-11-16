using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform playerObject;
    Vector3 offset;

    void Start()
    {
        offset = transform.position - playerObject.position;
    }

    void LateUpdate()
    {
        Vector3 newPosition = new Vector3(transform.position.x, transform.position.y, offset.z + playerObject.position.z);
        transform.position = Vector3.Lerp(transform.position, newPosition, 10 * Time.deltaTime);
    }

    public void InstantUpdate()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, offset.z + playerObject.position.z);
    }

    public void ChangeOffset(Vector2 newOffset)
    {
        offset = new Vector3(offset.x, newOffset.x, newOffset.y);
    }
}

using UnityEngine;

public class QuadRotation : MonoBehaviour
{
    public bool rotateClockwise = true;
    public float rotationSpeed = 5.0f;

    void Update()
    {
        // Choose rotation direction based on the boolean parameter
        float rotationDirection = rotateClockwise ? -1.0f : 1.0f;

        // Rotate the quad around the Z-axis
        transform.Rotate(Vector3.forward * rotationDirection * rotationSpeed * Time.deltaTime);
    }
}
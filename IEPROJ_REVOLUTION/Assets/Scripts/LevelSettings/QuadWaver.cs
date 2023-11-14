using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuadWaver : MonoBehaviour
{
    public float speed = 1.0f; // Controls the speed of the wave.
    public float amplitudeY = 1.0f; // Controls the height of the wave in the Y direction.
    public float amplitudeZ = 1.0f; // Controls the distance of the wave in the Z direction.

    private Vector3 originalPosition;

    void Start()
    {
        originalPosition = transform.position;
    }

    void Update()
    {
        // Calculate the new Y and Z positions based on sine waves.
        float newY = originalPosition.y + Mathf.Sin(Time.time * speed) * amplitudeY;
        float newZ = originalPosition.z + Mathf.Sin(Time.time * speed) * amplitudeZ;

        // Update the position of the Quad to create the wave effect.
        transform.position = new Vector3(transform.position.x, newY, newZ);
    }
}

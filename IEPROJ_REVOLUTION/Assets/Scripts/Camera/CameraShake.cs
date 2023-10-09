using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] private AnimationCurve curve;
    [SerializeField] private float duration = 1f;

    public static CameraShake instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public IEnumerator Shake()
    {
        //Vector3 startPos = transform.position;
        float elapsedTime = 0.0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float strength = curve.Evaluate(elapsedTime/duration);
            Vector3 temp = transform.localPosition;
            temp.x = temp.x + Random.Range(-.15f, 0.15f) * strength;
            temp.y = temp.y + Random.Range(-.15f, 0.15f) * strength;
            transform.localPosition = temp;
            //transform.position = transform.position + Random.insideUnitSphere * strength;
            yield return null;
        }

        transform.localPosition = new Vector3(0,0,0);
    }
}

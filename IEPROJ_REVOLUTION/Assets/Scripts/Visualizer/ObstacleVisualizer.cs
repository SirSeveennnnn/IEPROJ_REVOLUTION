using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleVisualizer : VisualizerBase
{
    private Vector3 startScale;
    [SerializeField] private float pulseIntensity = 2f;
    [SerializeField] private float returnSpeed = 2f;
    [SerializeField] private bool isText = false;

    private void Start()
    {
        startScale = transform.localScale;
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        if (isBeat)
        {
            return;
        }
        transform.localScale = Vector3.Lerp(transform.localScale, startScale, Time.deltaTime * returnSpeed);
    }

    public override void OnBeat()
    {
        base.OnBeat();
        Pulse();
    }

    private void Pulse()
    {
        //Debug.Log("Pulse");

        if (!isText)
        {
            Vector3 temp = new Vector3(1, 1 * pulseIntensity, 1);
            transform.localScale = temp;
        }
        else
        {
            transform.localScale = startScale * pulseIntensity;
        }


        isBeat = false;
    }
}

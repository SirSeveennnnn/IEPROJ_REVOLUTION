using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeonPath : MonoBehaviour
{
    public Color color = Color.white;
    public Color startColor = Color.white;

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = startColor;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            spriteRenderer.color = color;
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            spriteRenderer.color = startColor;
        }
    }
}

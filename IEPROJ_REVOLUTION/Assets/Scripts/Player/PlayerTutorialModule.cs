using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTutorialModule : MonoBehaviour
{
    TutorialHandler handler;

    private void Start()
    {
        handler = GameObject.Find("TutorialObjects").GetComponent<TutorialHandler>();  
    }

    private void OnTriggerEnter(Collider other)
    {
      
        if (other.CompareTag("TutorialTrigger"))
        {
            Debug.Log("Shi");
            handler.OpenTutorialWindow();
        }
    }

}

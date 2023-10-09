using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
   
    public GameObject gameOverPanel;
    // Start is called before the first frame update
    void Start()
    {
         
        gameObject.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerMovement.isCollided)
        {
            gameObject.SetActive(true);
        }
        
    }
    
}

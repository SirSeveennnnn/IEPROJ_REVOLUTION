using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static bool isCollided;
    public GameObject gameOverPanel;
    // Start is called before the first frame update
    void Start()
    {
        isCollided = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(isCollided == true)
        {
            Time.timeScale = 0;
        }
    }
    
}

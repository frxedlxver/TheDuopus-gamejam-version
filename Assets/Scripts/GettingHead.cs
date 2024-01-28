using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Getting_Head : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ManTrigger"))
        {
            //Switch Sprite for Man
            
            //Show timer leaderboard
        }
    }
}

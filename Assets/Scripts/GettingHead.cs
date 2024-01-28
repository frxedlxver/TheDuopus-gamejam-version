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
            Transform parentTransform = other.transform.parent;
            SpriteChanger changeSpriteScript = parentTransform.GetComponent<SpriteChanger>();
            
            if (changeSpriteScript )
            {
                changeSpriteScript.ChangeSpriteToNewSprite();
            }
            else
            {
                Debug.LogError("SpriteChanger script component not found on the parent GameObject.");
            }
            
            Time.timeScale = 0f;
            
            //Show timer leaderboard
        }
    }
}

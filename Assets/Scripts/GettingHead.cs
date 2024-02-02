using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Getting_Head : MonoBehaviour
{
    private object gameOverEvent;

    /*    public UnityEvent gameOverEvent = new UnityEvent();*/

    void Start()
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
        }
    }
}

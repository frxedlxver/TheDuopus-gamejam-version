using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteChanger : MonoBehaviour
{
    
    private SpriteRenderer spriteRenderer;
    public Sprite newSprite;

    
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Check if the SpriteRenderer component is found
        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer component not found on this GameObject.");
        }
    }

    void ChangeSpriteToNewSprite()
    {
        // Check if the newSprite is assigned
        if (newSprite != null)
        {
            // Change the sprite of the SpriteRenderer to the newSprite
            spriteRenderer.sprite = newSprite;
        }
        else
        {
            Debug.LogError("New sprite is not assigned. Please assign a sprite in the inspector.");
        }
    }
}

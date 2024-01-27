using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
    
    [SerializeField] private Rigidbody2D leftRb;
    [SerializeField] private Rigidbody2D rightRb;
    [SerializeField] private Rigidbody2D centerRb;

    private PlayerInput _input;

    private void Start()
    {

        _input = gameObject.GetComponent<PlayerInput>();
        if (_input == null)
        {
            Debug.LogWarning("Player object is missing PlayerInput component.");
        }
        if (!leftRb)
        {
            Debug.LogWarning("Missing reference to left RB");
        }
        if (!rightRb)
        {
            Debug.LogWarning("Missing reference to right RB");
        }
        if (!centerRb)
        {
            Debug.LogWarning("Missing reference to center RB");
        }
        
    }

    private void FixedUpdate()
    {
        Debug.Log("Left Stick: " + _input.MoveLeftValue);
        Debug.Log("Right Stick: " + _input.MoveRightValue);
        Debug.Log("Left Trigger: " + _input.SuctionLeftHeld);
        Debug.Log("Right Trigger: " + _input.SuctionRightHeld);
    }
    
}

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


    public Sucker L_sucker;
    private Vector2 L_suckerPos;
    private bool L_sucked;
    public Sucker R_sucker;
    private Vector2 R_suckerPos;
    private bool R_sucked;

    private SpriteRenderer L_suckerIndicator;
    private SpriteRenderer R_suckerIndicator;

    public float range = 10f;
    public Transform tipL;
    public Transform baseL;
    public Transform tipR;
    public Transform baseR;

    private Vector2 L_lastTipOffset;
    private Vector2 R_lastTipOffset;

    public float tentacleGravity = 6f;
    public float bodyGravity = 8f;

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
        if (_input.SuctionLeftHeld && L_sucker.IsSucking)
        {
            if (L_sucked)
            {
                L_sucker.transform.position = L_suckerPos;
                ApplyForceToBody(_input.MoveLeftValue);
            } else
            {
                L_suckerPos = L_sucker.transform.position;
                L_sucked = true;
            }
        } else
        {
            PlaceTip(_input.MoveLeftValue, tipL, baseL, leftRb);
        }

        if (_input.SuctionRightHeld && L_sucker.IsSucking)
        {
            L_suckerPos = L_sucker.transform.position;
        } else
        {
            PlaceTip(_input.MoveRightValue, tipR, baseR, rightRb);
        }
        
        
    }

    private void ApplyForceToBody()
    {
    }

    public void PlaceTip(Vector2 input, Transform tip, Transform baseT, Rigidbody2D rb)
    {
        Vector2 curOffset = (rb.position - (Vector2) baseT.position);

        Debug.Log(curOffset);



        if (input == Vector2.zero)
        {   
            rb.gravityScale = tentacleGravity;
        }
        else
        {
            rb.gravityScale = 0;
            curOffset = input * range;

            Debug.Log(range);
            Debug.Log(curOffset);
        }

        rb.position = (Vector2)(baseT.position) + Vector2.ClampMagnitude(curOffset, range);

    }
    

}

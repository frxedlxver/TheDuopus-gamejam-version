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
    public float headForceFactor = 10f;
    public float maxHeadForce = 50f;
    public float tentacleForce = 50f;

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
        if (_input.SuctionLeftHeld && L_sucker.CanSuck)
        {
            if (L_sucked)
            {
                L_sucker.transform.position = L_suckerPos;

            } else
            {
                L_suckerPos = L_sucker.transform.position;
                L_sucked = true;
            }
        } else
        {
            L_sucked = false;
            PlaceTip(_input.MoveLeftValue, tipL, baseL, leftRb);
        }

        if (_input.SuctionRightHeld && R_sucker.CanSuck)
        {
            if (R_sucked)
            {
                R_sucker.transform.position = R_suckerPos;

            }
            else
            {
                R_suckerPos = R_sucker.transform.position;
                R_sucked = true;
            }
        } else
        {
            R_sucked = false;
            PlaceTip(_input.MoveRightValue, tipR, baseR, rightRb);
        }


        if ((R_sucked && _input.MoveLeftValue != Vector2.zero) || L_sucked && _input.MoveRightValue != Vector2.zero)
        {
            centerRb.gravityScale = 0;
            Vector2 headTargetPos = (R_sucker.transform.position + L_sucker.transform.position) / 2;
            Vector2 forceDirection = headTargetPos - centerRb.position;
            float distance = forceDirection.magnitude;

            // Scale the force based on distance (with damping and force limit)
            float forceScale = Mathf.Min(distance * headForceFactor, maxHeadForce);
            forceDirection = forceDirection.normalized * forceScale;

            centerRb.AddForce(forceDirection);

        } else
        {
            centerRb.gravityScale = bodyGravity;
        }
    }

    public void PlaceTip(Vector2 input, Transform tip, Transform baseT, Rigidbody2D rb)
    {
        Vector2 curOffset = (rb.position - (Vector2)baseT.position);

        if (input == Vector2.zero)
        {
            rb.gravityScale = tentacleGravity;
        }
        else
        {
            rb.gravityScale = 0;
            Vector2 targetOffset = input * range;
            Vector2 targetPosition = (Vector2)baseT.position + targetOffset;
            Vector2 forceDirection = targetPosition - rb.position;

            // Apply a force towards the target position
            rb.AddForce(forceDirection.normalized * tentacleForce, ForceMode2D.Force);
        }
    }


}

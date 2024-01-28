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
    public float range = 10f;

    public float staminaSeconds = 10f;


    public Transform tipL;
    public Transform baseL;
    public Transform tipR;
    public Transform baseR;


    public float tentacleGravity = 6f;
    public float headGravity = 8f;

    private void Start()
    {
        _input = gameObject.GetComponent<PlayerInput>();
        L_sucker.maxStamina = staminaSeconds;
        R_sucker.maxStamina = staminaSeconds;
    }

    private void FixedUpdate()
    {
        HandleSucker(_input.SuctionLeftHeld, L_sucker, _input.MoveLeftValue, tipL, baseL, leftRb, ref L_suckerPos, ref L_sucked);
        HandleSucker(_input.SuctionRightHeld, R_sucker, _input.MoveRightValue, tipR, baseR, rightRb, ref R_suckerPos, ref R_sucked);

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
            centerRb.gravityScale = headGravity;
        }
    }

    private void HandleSucker(bool suctionHeld, Sucker sucker, Vector2 moveValue, Transform tip, Transform baseT, Rigidbody2D rb, ref Vector2 suckerPos, ref bool sucked)
    {
        if (suctionHeld && sucker.CanSuck)
        {
            if (sucked)
            {
                sucker.transform.position = suckerPos;
            }
            else
            {
                suckerPos = sucker.transform.position;
                sucker.Suck();
                sucked = true;
            }
        }
        else
        {
            sucked = false;
            sucker.StopSucking();
            PlaceTip(moveValue, tip, baseT, rb);
        }
    }

    public void PlaceTip(Vector2 input, Transform tip, Transform baseT, Rigidbody2D rb)
    {

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

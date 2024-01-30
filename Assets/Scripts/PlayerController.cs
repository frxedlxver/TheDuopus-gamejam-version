using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] public Rigidbody2D leftRb;
    [SerializeField] public Rigidbody2D rightRb;
    [SerializeField] public Rigidbody2D centerRb;

    public PlayerInput _input;

    public Sucker L_sucker;
    public Vector2 L_suckerPos;
    public bool L_sucked;
    public Sucker R_sucker;
    public Vector2 R_suckerPos;

    public Transform L_effector;
    public Transform R_effector;

    public bool R_sucked;
    public float headForceFactor = 10f;
    public float maxHeadForce = 50f;
    public float tentacleForce = 50f;
    public float range = 10f;
    public float suctionBreakDistance = 0.5f;

    public float staminaSeconds = 10f;

    public Transform tipL;
    public Transform baseL;
    public Transform tipR;
    public Transform baseR;

    public float tentacleGravity = 6f;
    public float headGravity = 8f;

    public void Start()
    {
        _input = gameObject.GetComponent<PlayerInput>();
        L_sucker.OtherSucker = R_sucker;
        R_sucker.OtherSucker = L_sucker;
    }

    public void FixedUpdate()
    {
        HandleSucker(_input.SuctionLeftHeld, _input.SuctionLeftPressed, L_sucker, L_effector, _input.MoveLeftValue, tipL, baseL, leftRb, ref L_suckerPos, ref L_sucked);
        HandleSucker(_input.SuctionRightHeld, _input.SuctionRightPressed, R_sucker, R_effector, _input.MoveRightValue, tipR, baseR, rightRb, ref R_suckerPos, ref R_sucked);

        if ((R_sucked && _input.MoveLeftValue != Vector2.zero) || (L_sucked && _input.MoveRightValue != Vector2.zero))
        {
            centerRb.gravityScale = 0;
            Vector2 headTargetPos = (leftRb.transform.position + rightRb.transform.position) / 2;
            Vector2 newPosition = Vector2.MoveTowards(centerRb.position, headTargetPos, maxHeadForce * Time.fixedDeltaTime);

            centerRb.MovePosition(newPosition);
        }
        else
        {
            centerRb.gravityScale = headGravity;
        }
    }

    public void HandleSucker(bool suctionHeld, bool suctionPressed, Sucker sucker, Transform effector, Vector2 moveValue, Transform target, Transform baseT, Rigidbody2D rb, ref Vector2 lockedPosition, ref bool sucked)
    {
        if (suctionPressed && sucker.CanSuck && !sucked)
        {
            GameObject touchedSuckable = sucker.touchedSuckable;
            if (touchedSuckable != null)
            {
                sucked = true;
                sucker.Suck();
                // Calculate the position of the sucker, local to touchedSuckable
                lockedPosition = touchedSuckable.transform.InverseTransformPoint(sucker.transform.position);
            }
        }

        if (sucked)
        { 
            GameObject touchedSuckable = sucker.touchedSuckable;
            if (touchedSuckable != null)
            {
                // Calculate the current world position of the locked position
                Vector2 currentLockedWorldPosition = touchedSuckable.transform.TransformPoint(lockedPosition);
                bool maxSuctionDistanceExceeded = Vector2.Distance(sucker.transform.position, currentLockedWorldPosition) > suctionBreakDistance;

                if (sucker.OtherSucker.JustStartedSucking || maxSuctionDistanceExceeded || !suctionHeld)
                {
                    sucked = false;
                    sucker.StopSucking();
                }
                else
                {
                    // Move the sucker towards the current world position of the locked position
                    rb.MovePosition(currentLockedWorldPosition);
                }
            } else
            {
                sucked = false;
                sucker.StopSucking();
            }

        }

        if (!sucked)
        {
            PlaceTip(moveValue, target, baseT, rb);
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
            Vector2 newPosition = Vector2.MoveTowards(rb.position, targetPosition, tentacleForce * Time.fixedDeltaTime);

            rb.MovePosition(newPosition);
        }
    }
}

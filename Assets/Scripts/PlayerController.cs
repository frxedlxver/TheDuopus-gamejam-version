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
        L_sucker.maxStamina = staminaSeconds;
        R_sucker.maxStamina = staminaSeconds;
    }

    public void FixedUpdate()
    {
        HandleSucker(_input.SuctionLeftHeld, L_sucker, L_effector, _input.MoveLeftValue, tipL, baseL, leftRb, ref L_suckerPos, ref L_sucked);
        HandleSucker(_input.SuctionRightHeld, R_sucker, R_effector, _input.MoveRightValue, tipR, baseR, rightRb, ref R_suckerPos, ref R_sucked);

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

    public void HandleSucker(bool suctionHeld, Sucker sucker, Transform effector, Vector2 moveValue, Transform target, Transform baseT, Rigidbody2D rb, ref Vector2 suckerPos, ref bool sucked)
    {
        if (suctionHeld && sucker.CanSuck && !sucked)
        {
            sucked = true;
        }

        if (!suctionHeld)
        {
            if (sucked)
            {
            }
            sucked = false;
        }

        if (sucked) {
            sucker.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;
            target.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;
            effector.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;
            sucker.Suck();
        }
        else
        {
            sucker.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
            target.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
            effector.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
            sucker.StopSucking();
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

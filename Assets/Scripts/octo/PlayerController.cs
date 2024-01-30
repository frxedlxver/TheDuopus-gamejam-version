using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] public Rigidbody2D HeadBoneRb;

    [Header("Left Tentacle")]
    public Sucker L_sucker;
    public Transform L_IKEffector;
    public Transform L_IKTarget;
    public Transform L_shoulderBone;


    [Header("Right Tentacle")]
    public Sucker R_sucker;
    public Transform R_IKEffector;
    public Transform R_IKTarget;
    public Transform R_shoulderBone;

    [Header("Force Params")]
    public float headForceFactor = 10f;
    public float maxHeadForce = 50f;
    public float tentacleForce = 50f;
    public float range = 10f;


    [Header("Gravity Values")]
    public float tentacleGravity = 6f;
    public float headGravity = 8f;

    // cached variables and flags
    private PlayerInput _input;
    private bool L_sucked;
    private bool R_sucked;
    private Rigidbody2D L_IKTargetRb;
    private Rigidbody2D R_IKTargetRb;

    public Rigidbody2D GetL_IKTargetRb()
    {
        return L_IKTargetRb;
    }

    public void Start()
    {
        _input = gameObject.GetComponent<PlayerInput>();
        L_IKTargetRb = L_IKTarget.GetComponent<Rigidbody2D>();
        R_IKTargetRb = R_IKTarget.GetComponent<Rigidbody2D>();
    }

    public void FixedUpdate()
    {
        
        // left sucker
        HandleSucker(_input.SuctionLeftHeld, _input.SuctionLeftPressed, L_sucker, L_IKEffector, _input.MoveLeftValue, L_IKTarget, L_shoulderBone, L_IKTargetRb);
        
        // right sucker
        HandleSucker(_input.SuctionRightHeld, _input.SuctionRightPressed, R_sucker, R_IKEffector, _input.MoveRightValue, R_IKTarget, R_shoulderBone, R_IKTargetRb);

        if ((R_sucker.SuckingTerrain && _input.MoveLeftValue != Vector2.zero) || (L_sucker.SuckingTerrain && _input.MoveRightValue != Vector2.zero))
        {
            HeadBoneRb.gravityScale = 0;
            Vector2 headTargetPos = (L_IKTargetRb.transform.position + R_IKTargetRb.transform.position) / 2;
            Vector2 newPosition = Vector2.MoveTowards(HeadBoneRb.position, headTargetPos, maxHeadForce * Time.fixedDeltaTime);

            HeadBoneRb.MovePosition(newPosition);
        }
        else
        {
            HeadBoneRb.gravityScale = headGravity;
        }
    }

    public void HandleSucker(bool suctionHeld, bool suctionPressed, Sucker sucker, Transform effector, Vector2 moveValue, Transform target, Transform baseT, Rigidbody2D rb)
    {
        if (suctionPressed && sucker.CanSuck && !sucker.Sucking)
        {
            sucker.Suck();
        }

        if (!suctionHeld)
        {
            sucker.StopSucking();
        }

        if (sucker.SuckingTerrain) {
            sucker.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;
            target.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;
            effector.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;
        }
        else
        {
            sucker.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
            target.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
            effector.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
            PlaceTip(moveValue, target, baseT, rb);
        }
    }

    public void PlaceTip(Vector2 input, Transform tip, Transform baseT, Rigidbody2D rb)
    {
            Vector2 targetOffset = input * range;
            Vector2 targetPosition = (Vector2)baseT.position + targetOffset;
            Vector2 newPosition = Vector2.MoveTowards(rb.position, targetPosition, tentacleForce * Time.fixedDeltaTime);

            rb.MovePosition(newPosition);
    }
}

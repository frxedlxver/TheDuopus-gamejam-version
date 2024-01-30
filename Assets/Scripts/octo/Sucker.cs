using Unity.VisualScripting;
using UnityEngine;

public class Sucker : MonoBehaviour
{
    private SpriteRenderer sprite;
    private Rigidbody2D rb;
    private Vector3 targetLocalPosition;

    // cache a reference to the other sucker
    public Sucker OtherSucker;
    public Color suctionIndicatorColor;

    public bool Sucking { get; private set; }
    public bool SuckingTerrain { get { return Sucking && touchingTerrain; } }
    public bool CanSuck { get { return TouchedSuckableObject != null; } }
    public bool CanSuckTerrain
    {
        get
        {
            return CanSuck && touchingTerrain;
        }
    }

    private bool touchingTerrain { get { return TouchedSuckableObject != null && TouchedSuckableObject.layer == LayerMask.NameToLayer("terrain"); } }

    public GameObject TouchedSuckableObject;

    public void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        this.targetLocalPosition = this.transform.localPosition;
    }

    public void Update()
    {
        if (this.transform.localPosition != targetLocalPosition)
        {
            rb.MovePosition(targetLocalPosition);
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (ObjectIsSuckable(collision.gameObject))
        {
            TouchedSuckableObject = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (ObjectIsSuckable(collision.gameObject) && collision.gameObject == TouchedSuckableObject)
        {
            TouchedSuckableObject = null;
        }
    }

    public void Suck()
    {
        Sucking = true;
        sprite.enabled = true;
    }

    public void StopSucking()
    {
        Sucking = false;
        sprite.enabled = false;
    }

    private bool ObjectIsSuckable(GameObject g)
    {
        return g.layer == LayerMask.NameToLayer("terrain");

    }
}

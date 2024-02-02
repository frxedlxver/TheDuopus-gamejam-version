using UnityEngine;

public class Sucker : MonoBehaviour
{

    private Vector3 lockedLocalPos;
    private SpriteRenderer sprite;
    public Color c;

    public Sucker OtherSucker;
    public bool JustStartedSucking { get { return framesSinceStartedSucking < 3;  } }
    private int framesSinceStartedSucking = 0;

    public bool Sucking { get; private set; }
    private bool touchingSuckable;
    public GameObject touchedSuckable;
    public bool CanSuck
    {
        get { return touchingSuckable; }
    }
    public void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        this.lockedLocalPos = this.transform.localPosition;
    }

    public void Update()
    {
        if (this.transform.localPosition != lockedLocalPos)
        {
            this.transform.localPosition = lockedLocalPos;
        }
    }

    public void FixedUpdate() {
        if (framesSinceStartedSucking < 3 )
        {
            framesSinceStartedSucking++;
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("terrain"))
        {
            touchedSuckable = collision.gameObject;
            touchingSuckable = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        
        if (touchedSuckable != null && collision.gameObject == touchedSuckable)
        {
            touchedSuckable = null;
            touchingSuckable = false;
        }
    }

    public void Suck()
    {
        framesSinceStartedSucking = 0;
        Sucking = true;
        sprite.enabled = true;
    }

    public void StopSucking()
    {
        Sucking = false;
        sprite.enabled = false;
    }
}

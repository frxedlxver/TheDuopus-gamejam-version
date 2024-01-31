using UnityEngine;

public class Sucker : MonoBehaviour
{

    private SpriteRenderer suctionSprite;
    public Color suctionPointColor;

    public Sucker OtherSucker;
    public Vector2 SuckPosition = Vector2.zero;
    public GameObject suctionPoint;
    public bool JustStartedSucking { get { return framesSinceStartedSucking < 3;  } }
    private int framesSinceStartedSucking = 0;

    public bool Sucking { get; private set; }

    private void Start()
    {
        suctionSprite = suctionPoint.GetComponent<SpriteRenderer>();
        suctionSprite.color = suctionPointColor;
        suctionPoint.SetActive(false);
    }

    private void Update()
    {
        suctionSprite.color = suctionPointColor;
    }
    public bool TouchingSuckable
    {
        get { return TouchedSuckable != null; }
    }
    public GameObject TouchedSuckable;

    // this is the same as Touching suckable, but here because we plan to add stamina. 
    public bool CanSuck
    {
        get { return TouchingSuckable; }
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
            TouchedSuckable = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        
        if (TouchingSuckable && collision.gameObject == TouchedSuckable)
        {
            TouchedSuckable = null;
        }
    }

    public void Suck()
    {
        if (TouchingSuckable)
        {
            suctionPoint.transform.parent = TouchedSuckable.transform;
            suctionPoint.transform.position = this.transform.position;
            suctionPoint.SetActive(true);
            framesSinceStartedSucking = 0;
            Sucking = true;
        }

    }

    public void StopSucking()
    {
        if (Sucking)
        {
            suctionPoint.transform.parent = this.transform;
            suctionPoint.SetActive(false);
            suctionPoint.transform.position = this.transform.position;
            Sucking = false;
        }

    }
}

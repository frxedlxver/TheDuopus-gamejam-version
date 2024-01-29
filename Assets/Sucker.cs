using UnityEngine;

public class Sucker : MonoBehaviour
{

    private Vector3 lockedLocalPos;
    private SpriteRenderer sprite;
    public Color c;

    public bool Sucking { get; private set; }
    private bool touchingSuckable;
    public bool CanSuck
    {
        get { return touchingSuckable; }
    }
    public float maxStamina;
    public float curStamina { get; private set; }
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
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("terrain"))
        {
            
            touchingSuckable = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        
        if (collision.gameObject.layer == LayerMask.NameToLayer("terrain"))
        {
            
            touchingSuckable = false;
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
}

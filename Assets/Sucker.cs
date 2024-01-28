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
        get { return touchingSuckable && curStamina > 0; }
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
        if (Sucking)
        {
            curStamina = curStamina - Time.deltaTime;
        } 
        else
        {
            curStamina = Mathf.Max(curStamina + Time.deltaTime, maxStamina);
        }

        Debug.Log(this.gameObject.name + " stamina: " + curStamina.ToString());
        if (this.transform.localPosition != lockedLocalPos)
        {
            this.transform.localPosition = lockedLocalPos;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        sprite.enabled = true;
        if (collision.gameObject.layer == LayerMask.NameToLayer("terrain"))
        {
            touchingSuckable = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        sprite.enabled = false;
        if (collision.gameObject.layer == LayerMask.NameToLayer("terrain"))
        {
            touchingSuckable = false;
        }
    }

    public void Suck()
    {
        Sucking = true;
        sprite.color = new Color(c.r, c.g, c.b, 1);
    }

    public void StopSucking()
    {
        Sucking = false;
        sprite.color = new Color(c.r, c.g, c.b, 0.15f);
    }
}

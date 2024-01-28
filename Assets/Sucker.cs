using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sucker : MonoBehaviour
{

    private Vector3 lockedLocalPos;
    public bool CanSuck { get; private set; }

    public void Start()
    {
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
            CanSuck = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("terrain"))
        {
            CanSuck = false;
        }
    }
}

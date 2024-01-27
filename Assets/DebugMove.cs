using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugMove : MonoBehaviour
{
    public Rigidbody2D rb;
    public float maxSpeed = 10f;
    public float defaultForce = 30f;
    public float forceToAdd = 0f;

    void Start()
    {
        rb = GetComponent <Rigidbody2D>();
    }

    void Update()
    {
        float currentSpeed = rb.velocity.magnitude;


        if (currentSpeed > (maxSpeed - (maxSpeed / 4))) {
            // we are going fast enough to limit the speed
            float forceMultiplier = defaultForce * maxSpeed - (currentSpeed / maxSpeed);
            forceToAdd = forceMultiplier;
        } else
        {
            forceToAdd = defaultForce;
        }
    }
}

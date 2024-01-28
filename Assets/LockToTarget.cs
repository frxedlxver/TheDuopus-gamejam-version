using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockToTarget : MonoBehaviour
{
    public Transform target;

    void FixedUpdate()
    {
        this.transform.position = target.position;
    }
}

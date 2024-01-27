using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterRipple : MonoBehaviour
{
    private Material material; 

    void Start()
    {
        Renderer renderer = GetComponent<Renderer>();
        material = renderer.material;
    }

    void Update()
    {
        float time = Time.time; 
        material.SetFloat("_WaterTime", time);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchExplosion : MonoBehaviour
{
    void Start()
    {
        gameObject.AddComponent<CircleCollider2D>();
    }

    private void OnCollisionEnter(Collision other)
    {
        // BLABLABLA
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchExplosion : MonoBehaviour
{
    public ProjectileData projectileData;
    public bool actif;
    
    void Start()
    {
        gameObject.AddComponent<CircleCollider2D>();
        actif = false;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (actif)
        {
            other.gameObject.GetComponent<Rigidbody2D>().velocity = gameObject.transform.right * projectileData.Force * 0.075f;
            Debug.Log("aaaaaa");
        }
        Debug.Log("aqszaa");
    }
}

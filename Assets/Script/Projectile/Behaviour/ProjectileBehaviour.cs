using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ProjectileBehaviour : MonoBehaviour
{
    
    private Rigidbody2D rb;

    public GameObject Prefab;

    public ProjectileData projectileData;
    
    public void Start()
    {
        rb = gameObject.AddComponent<Rigidbody2D>();
        CircleCollider2D circle = gameObject.AddComponent<CircleCollider2D>();
        circle.isTrigger = true;
        SetStraightVelocity();
    }

    public void SetPrefab(GameObject prefab)
    {
        Prefab = prefab;
    }
    
    public void SetStraightVelocity()
    {
        rb.velocity =  gameObject.transform.right * projectileData.Force;
    }
}

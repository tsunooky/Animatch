using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Script.Manager;

public abstract class ProjectileBehaviour : MonoBehaviour
{
    
    private Rigidbody2D rb;

    public GameObject Prefab;

    public ProjectileData projectileData;

    public bool declanchement;
    
    public void Start()
    {
        declanchement = false;
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

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Script.Manager;

public abstract class ProjectileBehaviour : MonoBehaviour
{

    public GameObject Prefab;

    public ProjectileData projectileData;

    public bool declanchement;

    public float lauchForce;
    
    public void Start()
    {
        declanchement = false;
        CircleCollider2D circle = gameObject.AddComponent<CircleCollider2D>();
        circle.isTrigger = true;
    }

    public void Set(float LauchfForce, ProjectileData proj)
    {
        projectileData = proj;
        Prefab = proj.Explosion;
        lauchForce = LauchfForce;
        SetStraightVelocity();
    }
    
    
    public void SetStraightVelocity()
    {
        gameObject.GetComponent<Rigidbody2D>().velocity = gameObject.transform.right * projectileData.Force * lauchForce * 0.075f;
    }

    protected void FinAction()
    {
        GameManager.Instance.playerActif.enAction = false;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Script.Manager;
using Unity.VisualScripting;

public abstract class ProjectileBehaviour : MonoBehaviour
{

    public GameObject Prefab;

    public ProjectileData projectileData;

    public bool declanchement;

    public (Vector2,Vector2) ligne;

    public Rigidbody2D rb;
    
    public void Start()
    {
        declanchement = false;
        CircleCollider2D circle = gameObject.AddComponent<CircleCollider2D>();
        circle.isTrigger = true;
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    public void Set((Vector2,Vector2)Ligne, ProjectileData proj)
    {
        projectileData = proj;
        Prefab = proj.Explosion;
        ligne = Ligne;
        SetStraightVelocity();
    }
    
    
    public void SetStraightVelocity()
    {
        gameObject.GetComponent<Rigidbody2D>().velocity = (ligne.Item1 - ligne.Item2) * (projectileData.Force * 0.075f);;
    }
    
    public void SetDirection(Vector2 targetPosition, ProjectileData proj, float velocityMultiplier = 1.0f)
    {
        projectileData = proj;
        Prefab = proj.Explosion;

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        
        Vector2 direction = targetPosition - (Vector2)transform.position;
        direction.Normalize();
        
        rb.velocity = direction * (proj.Force * 0.75f * velocityMultiplier);
        
        Debug.Log("Projectile direction: " + direction + " with velocity: " + rb.velocity + " (multiplier: " + velocityMultiplier + ")");
    }

    protected void FinAction()
    {
        GameManager.Instance.playerActif.enAction = false;
        if (GameManager.Instance.playerActif.drops == 0)
        {
            GameManager.Instance.FinDuTour();
        }
    }
    
    void Update()
    {
        if (rb.velocity.sqrMagnitude > 0.01f)
        {
            float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
            
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }
    }
}

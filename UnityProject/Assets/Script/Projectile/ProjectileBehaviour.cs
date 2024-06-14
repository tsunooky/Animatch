using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
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
    
    public void SetDirection(Vector2 targetPosition, ProjectileData proj)
    {
        
        projectileData = proj;
        Prefab = proj.Explosion;
        
        // Appliquer la direction et la vitesse au Rigidbody2D
        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        // Calculer la direction de la vélocité en direction de la position cible
        Vector2 direction = targetPosition - (Vector2)transform.position;
        direction.Normalize();

        // Appliquer la vélocité au projectile
        rb.velocity = direction * (proj.Force * 0.75f);
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
        // Si la vélocité est significative, ajustez la rotation
        if (rb.velocity.sqrMagnitude > 0.01f)
        {
            // Calculer l'angle de la vélocité
            float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;

            // Appliquer la rotation
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }
    }
}

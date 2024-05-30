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

    public (Vector2,Vector2) ligne;
    
    public void Start()
    {
        declanchement = false;
        CircleCollider2D circle = gameObject.AddComponent<CircleCollider2D>();
        circle.isTrigger = true;
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

    protected void FinAction()
    {
        GameManager.Instance.playerActif.enAction = false;
    }
}

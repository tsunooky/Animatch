using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks.Sources;
using Script.Manager;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class AimAndShoot : MonoBehaviour
{
    public ProjectileData projectileData;
    
    private Vector2 worldPosition;
    private Vector2 direction;

    private Sprite spriteGun;

    private GameObject gun;

    private float spawnDistance = 0.65f;

    void Awake()
    {
        spriteGun = Resources.Load<Sprite>("Sprites/Projectiles/Gun");
        projectileData = Resources.Load<ProjectileData>("Data/Projectile/Tomate");
    }
    
    private void Start()
    {
        // Appeler la fonction GetMouseCoordinates() chaque seconde (1f seconde)
        
        gun = new GameObject("Pistol");
        SpriteRenderer gunRenderer = gun.AddComponent<SpriteRenderer>();
        gun.AddComponent<DespawnManager>();
        gunRenderer.sortingOrder = 2;
        gunRenderer.sprite = spriteGun;
        InvokeRepeating("Aim", 0f, 1f / 60f);
    }
    
    private void Update()
    {
        Shoot();
    }

    private void Aim()
    {
        if (gun != null)
        {
            gun.transform.position = gameObject.transform.position;
            // Récupérer les coordonnées de la souris en pixels
            Vector2 mousePositionPixels = Mouse.current.position.ReadValue();

            // Convertir les coordonnées de la souris de pixels à des coordonnées dans l'espace du monde
            Vector2 mousePositionWorld = Camera.main.ScreenToWorldPoint(mousePositionPixels);
            direction = (mousePositionWorld - (Vector2)gun.transform.position).normalized;
            gun.transform.right = direction;
        }
    }

    private void Shoot()
    {
        if (gun != null)
        {
            if (Mouse.current.leftButton.wasReleasedThisFrame)
            {
                Vector3 direction = gun.transform.right;

                // Calculer la nouvelle position en ajoutant la direction multipliée par la distance
                Vector3 newPosition = gun.transform.position + direction * spawnDistance;
                
                BulletBehaviour.OnBulletDestroyed += OnDestroy;
                GameObject bullet = Instantiate(projectileData.Projectile, newPosition, gun.transform.rotation);
                BulletBehaviour bulletBehaviour = bullet.GetComponent<BulletBehaviour>();
                bulletBehaviour.projectileData = projectileData;
                bulletBehaviour.SetPrefab(projectileData.Explosion);
            }
        }
    }
    
    private void OnDestroy()
    {
        // Désabonner l'événement lors de la destruction du script
        BulletBehaviour.OnBulletDestroyed -= OnDestroy;
        Destroy(gun);
        Destroy(this);
    }
    
}


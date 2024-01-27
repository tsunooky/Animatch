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

    private GameObject bulletInst;
    
    private Vector2 worldPosition;
    private Vector2 direction;

    private Sprite spriteGun;

    private GameObject gun;

    private float spawnDistance = 0.65f;

    void Awake()
    {
        spriteGun = Resources.Load<Sprite>("Sprites/Projectiles/Gun");
        bulletInst = Resources.Load<GameObject>("Prefabs/Circle");
    }
    
    private void Start()
    {
        // Appeler la fonction GetMouseCoordinates() chaque seconde (1f seconde)
        
        gun = new GameObject("Pistol");
        SpriteRenderer gunRenderer = gun.AddComponent<SpriteRenderer>();
        gun.AddComponent<CameraManager>();
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

                Instantiate(bulletInst, newPosition, gun.transform.rotation);
            }
        }
    }
    
}


using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks.Sources;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class AimAndShoot : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform bulletSpawnPoint;

    private GameObject bulletInst;
    
    private Vector2 worldPosition;
    private Vector2 direction;

    private Sprite spriteGun;

    private GameObject gun;

    void Awake()
    {
        spriteGun = Resources.Load<Sprite>("Sprites/Projectiles/Gun");
        bullet = Resources.Load<GameObject>("Prefabs/Circle");
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
        gun.transform.position = gameObject.transform.position;
        // Récupérer les coordonnées de la souris en pixels
        Vector2 mousePositionPixels = Mouse.current.position.ReadValue();

        // Convertir les coordonnées de la souris de pixels à des coordonnées dans l'espace du monde
        Vector2 mousePositionWorld = Camera.main.ScreenToWorldPoint(mousePositionPixels);
        direction = (mousePositionWorld - (Vector2)gun.transform.position).normalized;
        
        gun.transform.right = direction;
    }

    private void Shoot()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            bulletInst = Instantiate(bullet, gun.transform.position, gun.transform.rotation);
        }
    }
    
}


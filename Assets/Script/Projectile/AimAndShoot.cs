using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AimAndShoot : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform bulletSpawnPoint;

    private GameObject bulletInst;
    
    private Vector2 worldPosition;
    private Vector2 direction;
    
    private void Start()
    {
        // Appeler la fonction GetMouseCoordinates() chaque seconde (1f seconde)
        InvokeRepeating("Aim", 0f, 1f / 60f);
    }

    private void Update()
    {
        Shoot();
    }

    private void Aim()
    {
        // Récupérer les coordonnées de la souris en pixels
        Vector2 mousePositionPixels = Mouse.current.position.ReadValue();

        // Convertir les coordonnées de la souris de pixels à des coordonnées dans l'espace du monde
        Vector2 mousePositionWorld = Camera.main.ScreenToWorldPoint(mousePositionPixels);
        
        direction = (mousePositionWorld - (Vector2)gameObject.transform.position).normalized;
        gameObject.transform.right = direction;
    }

    private void Shoot()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            bulletInst = Instantiate(bullet, bulletSpawnPoint.position, gameObject.transform.rotation);
        }
    }
    
}


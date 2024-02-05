using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks.Sources;
using Script.Manager;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;


public class Aim : MonoBehaviour
{
    public ProjectileData projectileData;
    
    private Vector2 worldPosition;
    private Vector2 direction;

    private Sprite spriteGun;

    private GameObject gun;

    private float spawnDistance = 0.65f;

    public void Initialize(string nameProjectile)
    {
        projectileData = Resources.Load<ProjectileData>("Data/Projectile/" + nameProjectile);
    }
    
    private void Start()
    {
        // Appeler la fonction GetMouseCoordinates() chaque seconde (1f seconde)
        
        gun = new GameObject("Pistol");
        gun.AddComponent<DespawnManager>();
        InvokeRepeating("Aim", 0f, 1f / 60f);
    }

    private void Update()
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
    
    private void OnDestroy()
    {
        Destroy(gun);
        GameManager.Instance.tourActif = false;
    }
    
}


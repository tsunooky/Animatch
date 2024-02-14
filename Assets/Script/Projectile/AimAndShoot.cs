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

    private float delayBeforeShootBOT = 5f;
    public bool bot;

    public void Initialize(string nameProjectile)
    {
        bot = false;
        projectileData = Resources.Load<ProjectileData>("Data/Projectile/" + nameProjectile);
    }

    void Awake()
    {
        spriteGun = Resources.Load<Sprite>("Sprites/Projectiles/Gun");
    }
    
    private void Start()
    {
        // Appeler la fonction GetMouseCoordinates() chaque seconde (1f seconde)

        if (!bot)
        {
            gun = new GameObject("Pistol");
            SpriteRenderer gunRenderer = gun.AddComponent<SpriteRenderer>();
            gun.AddComponent<DespawnManager>();
            gunRenderer.sortingOrder = 6;
            gunRenderer.sprite = spriteGun;
            InvokeRepeating("Aim", 0f, 1f / 60f);
        }
    }
    
    private void Update()
    {
        if (GameManager.Instance.playerActif.enAction)
        {
            if (!bot)
                Shoot();
        }
        else
        {
            Destroy(this);
        }
    }

    private void Aim()
    {
        if (gun != null && !bot)
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

    // ReSharper disable Unity.PerformanceAnalysis
    private void Shoot()
    {
        if (gun != null)
        {
            if (Mouse.current.leftButton.wasReleasedThisFrame && Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()).y > 2.5)
            {
                Vector3 direction = gun.transform.right;

                // Calculer la nouvelle position en ajoutant la direction multipliée par la distance
                Vector3 newPosition = gun.transform.position + direction * spawnDistance;
                GameObject bullet = Instantiate(projectileData.Projectile, newPosition, gun.transform.rotation);
                ProjectileBehaviour bulletBehaviour = bullet.GetComponentsInChildren<ProjectileBehaviour>()[0];
                bulletBehaviour.projectileData = projectileData;
                bulletBehaviour.SetPrefab(projectileData.Explosion);
                Destroy(this);
            }
        }
    }
    
    private void OnDestroy()
    {
        Destroy(gun);
    }
    
    public void Shoot(Vector3 direction)
    {
        StartCoroutine(ShootCoroutine(direction));
    }

    private IEnumerator ShootCoroutine(Vector3 direction)
    {
        // Attendre pendant le délai spécifié
        yield return new WaitForSeconds(delayBeforeShootBOT);
        // Calculer la nouvelle position en ajoutant la direction multipliée par la distance
        Vector3 newPosition = transform.position + direction * spawnDistance;
        GameObject bullet = Instantiate(projectileData.Projectile, newPosition, transform.rotation);
        ProjectileBehaviour bulletBehaviour = bullet.GetComponent<ProjectileBehaviour>();
        bulletBehaviour.projectileData = projectileData;
        bulletBehaviour.SetPrefab(projectileData.Explosion);
        Destroy(this);
    }
}


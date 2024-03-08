using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Script.Manager;
using UnityEngine.InputSystem;

public class HandToHand : MonoBehaviour
{
    public ProjectileData projectileData;
    private Vector2 direction;
    private GameObject gun;
    private Sprite spriteGun;

    private float spawnDistance = 0.70f;

    private float delayBeforeShootBOT = 5f;
    public bool bot;
    private bool isAiming;

    public void Initialize(ProjectileData ProjectileData, Sprite SpriteGun)
    {
        isAiming = false;
        bot = false;
        projectileData = ProjectileData;
        spriteGun = SpriteGun;
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
        if (!GameManager.Instance.playerActif.enAction)
        {
            Destroy(this);
        }
        if (Mouse.current.leftButton.wasPressedThisFrame && GameManager.Instance.playerActif.enVisee)
        {
            setAim(true,false);
        }
        if (Mouse.current.leftButton.wasReleasedThisFrame && isAiming && !GameManager.Instance.playerActif.enVisee)
        {
            if (!bot)
                Shoot();
        }
        
        if (Input.GetMouseButton(0) && isAiming)
        {
            // ACtivation de la l'affichage de la visée
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
            setAim(false,false);
            Vector3 direction = -gun.transform.right;

            // Calculer la nouvelle position en ajoutant la direction multipliée par la distance
            Vector3 newPosition = gun.transform.position + direction * spawnDistance;
            GameObject bullet = Instantiate(projectileData.Projectile, newPosition, gun.transform.rotation * Quaternion.Euler(0f, 180f, 0f));
            ProjectileBehaviour bulletBehaviour = bullet.GetComponentsInChildren<ProjectileBehaviour>()[0];
            //bulletBehaviour.Set(LauchfForce,projectileData);
            Destroy(this);
        }
    }
    
    private void OnDestroy()
    {
        //Destroy(affichage);
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
        //bulletBehaviour.Set(LauchfForce,projectileData);
        Destroy(this);
    }

    private void setAim(bool isAiming, bool aura)
    {
        GameManager.Instance.playerActif.SetAura(aura);
        this.isAiming = isAiming;
    }
    
}

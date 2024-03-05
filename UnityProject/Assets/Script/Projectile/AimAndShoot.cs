using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks.Sources;
using Script.Manager;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using Script.Manager;


public class AimAndShoot : MonoBehaviour
{
    public ProjectileData projectileData;
    
    private Vector2 worldPosition;
    private Vector2 direction;

    private Sprite spriteGun;

    private GameObject gun;

    private float spawnDistance = 0.70f;

    private float delayBeforeShootBOT = 5f;
    public bool bot;
    private bool isAiming;

    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private float lauchForce;
    [SerializeField] private float trajectoryTimeStep = 0.05f;
    [SerializeField] private int trajectoryStepCount = 15;
    private Vector2 startMousePos;
    private Vector2 currentMousePos;
    private Vector2 velocity;

    public void Initialize(string nameProjectile)
    {
        isAiming = false;
        bot = false;
        projectileData = Resources.Load<ProjectileData>("Data/Projectile/" + nameProjectile);
        lauchForce = projectileData.Force;
    }

    void Awake()
    {
        setLineRenderer();
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
            startMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        if (Mouse.current.leftButton.wasReleasedThisFrame && isAiming && !GameManager.Instance.playerActif.enVisee)
        {
            if (!bot)
                Shoot();
        }
        
        if (Input.GetMouseButton(0) && isAiming)
        {
            currentMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            velocity = (startMousePos - currentMousePos) * lauchForce;
            DrawTrajectory();
        }
    }

    private void DrawTrajectory()
    {
        Vector3[] positions = new Vector3[trajectoryStepCount];
        for (int i = 0; i < trajectoryStepCount; i++)
        {
            float t = i * trajectoryTimeStep;
            Vector3 pos = gameObject.transform.position + (Vector3) velocity * t;
            positions[i] = pos;
        }
        lineRenderer.SetPositions(positions);
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
            bulletBehaviour.projectileData = projectileData;
            bulletBehaviour.SetPrefab(projectileData.Explosion);
            Destroy(this);
        }
    }
    
    private void OnDestroy()
    {
        Destroy(lineRenderer);
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

    private void setAim(bool isAiming, bool aura)
    {
        GameManager.Instance.playerActif.SetAura(aura);
        this.isAiming = isAiming;
    }

    private void setLineRenderer()
    {
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.startColor = Color.green;
        lineRenderer.endColor = Color.green;
        lineRenderer.startWidth = 0.07f; // Largeur de début de la ligne
        lineRenderer.endWidth = 0.07f; // Largeur de fin de la ligne
        lineRenderer.sortingOrder = 100;
    }
}


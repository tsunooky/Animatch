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
    

    private GameObject gun;

    private float delayBeforeShootBOT = 5f;
    private bool isAiming;

    [SerializeField] private float lauchForce;
    [SerializeField] private float trajectoryTimeStep = 0.0125f;
    [SerializeField] private int trajectoryStepCount = 7;
    private Vector2 startMousePos;
    private Vector2 currentMousePos;
    private Vector2 velocity;
    private GameObject pointilleVisee;
    [SerializeField] private GameObject[] pointilles;
    private float mass;

    public void Initialize(ProjectileData ProjectileData, Sprite SpriteGun)
    {
        isAiming = false;
        projectileData = ProjectileData;
        lauchForce = projectileData.Force;
        Rigidbody2D rigidbody2D = projectileData.Projectile.GetComponent<Rigidbody2D>();
        mass = rigidbody2D.mass;
        
        gun = new GameObject("Pistol");
        SpriteRenderer gunRenderer = gun.AddComponent<SpriteRenderer>();
        gun.AddComponent<DespawnManager>();
        gunRenderer.sortingOrder = 6;
        gunRenderer.sprite = SpriteGun;
        InvokeRepeating("Aim", 0f, 1f / 60f);
    }
    
    public void Initialize(string nameProjectile)
    {
        isAiming = false;
        projectileData = Resources.Load<ProjectileData>("Data/Projectile/" + nameProjectile);
        lauchForce = projectileData.Force;
        Rigidbody2D rigidbody2D = projectileData.Projectile.GetComponent<Rigidbody2D>();
        mass = rigidbody2D.mass;
        
        gun = new GameObject("Pistol");
        SpriteRenderer gunRenderer = gun.AddComponent<SpriteRenderer>();
        gun.AddComponent<DespawnManager>();
        gunRenderer.sortingOrder = 6;
        gunRenderer.sprite = projectileData.Projectile.GetComponent<Sprite>();;
        InvokeRepeating("Aim", 0f, 1f / 60f);
    }

    void Awake()
    {
        pointilleVisee = Resources.Load<GameObject>("Prefabs/Autre/Pointille");
        pointilles = new GameObject[trajectoryStepCount];
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
            Shoot();
        }

        if (Input.GetMouseButton(0) && isAiming)
        {
            currentMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            velocity = (startMousePos - currentMousePos) * projectileData.Force * 0.5f * mass;
            DrawTrajectory();
        }
    }

    private void DrawTrajectory()
    {
        Vector3[] positions = new Vector3[trajectoryStepCount];
        foreach (GameObject obj in pointilles)
        {
            // Vérifier si l'objet est présent dans la scène
            if (obj != null)
            {
                // Détruire l'objet de la scène
                Destroy(obj);
            }
        }

        float scale = 0.2f;
        for (int i = 1; i < trajectoryStepCount; i++)
        {
            float t = i * trajectoryTimeStep;
            Vector3 pos = gameObject.transform.position + (Vector3) velocity * t  + (lauchForce + projectileData.Force) * mass * Physics.gravity * t * t;
            positions[i] = pos;
            pointilles[i] = Instantiate(pointilleVisee,positions[i],Quaternion.Euler(0f,0f,0f));
            pointilles[i].transform.localScale = new Vector3(scale, scale, 1);
            scale /= 1.2f;
        }
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

    // ReSharper disable Unity.PerformanceAnalysis
    private void Shoot()
    {
        if (gun != null)
        {
            setAim(false,false);
            projectileData.Lanceur = gameObject;
            Vector3 newPosition = gun.transform.position;
            GameObject bullet = Instantiate(projectileData.Projectile, newPosition, gun.transform.rotation * Quaternion.Euler(0f, 180f, 0f));
            ProjectileBehaviour bulletBehaviour = bullet.GetComponentsInChildren<ProjectileBehaviour>()[0];
            bulletBehaviour.Set((startMousePos,currentMousePos),projectileData);
            Destroy(this);
        }
    }
    
    private void OnDestroy()
    {
        int len = pointilles.Length;
        int i = 0;
        while (i < len)
        {
            Destroy(pointilles[i]);
            i++;
        }
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
        projectileData.Lanceur = gameObject;
        Vector3 newPosition = transform.position;
        GameObject bullet = Instantiate(projectileData.Projectile, newPosition, transform.rotation);
        ProjectileBehaviour bulletBehaviour = bullet.GetComponent<ProjectileBehaviour>();
        bulletBehaviour.Set((startMousePos,currentMousePos),projectileData);
        Destroy(this);
    }

    private void setAim(bool isAiming, bool aura)
    {
        GameManager.Instance.playerActif.SetAura(aura);
        this.isAiming = isAiming;
    }
    
}


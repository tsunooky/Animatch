using System.Collections;
using System.Collections.Generic;
using Script.Manager;
using UnityEngine;
using UnityEngine.InputSystem;

public class AimBehviour : MonoBehaviour
{
    public ProjectileData projectileData;
    
    private Vector2 direction;
    private Vector2 currentMousePos;
    private Vector2 velocity;
    
    private GameObject pointilleVisee;
    [SerializeField] private GameObject[] pointilles;
    private float mass;
    private float lauchForce;
    [SerializeField] private float trajectoryTimeStep = 0.0125f;
    [SerializeField] private int trajectoryStepCount = 7;
    private float delayBeforeShootBOT = 0.1f;
    private Tireur TireurBehaviour;

    public void Initialize(ProjectileData ProjectileData, Tireur tireurBehaviour)
    {
        TireurBehaviour = tireurBehaviour;
        projectileData = ProjectileData;
        lauchForce = projectileData.Force;
        
        Rigidbody2D rigidbody2D = projectileData.Projectile.GetComponent<Rigidbody2D>();
        mass = rigidbody2D.mass;
        InvokeRepeating("Aim", 0f, 1f / 60f);
    }
    
    public void Initialize(Tireur tireurBehaviour)
    {
        TireurBehaviour = tireurBehaviour;
        // TEMPORAIRE
        lauchForce = 10;
        mass = 1;
        InvokeRepeating("Aim", 0f, 1f / 60f);
    }

    void Awake()
    {
        if (GameManager.Instance.playerActif.animalActif is EagleBehaviour)
        {
            trajectoryStepCount = 14;
        }
        pointilleVisee = Resources.Load<GameObject>("Prefabs/Autre/Pointille");
        pointilles = new GameObject[trajectoryStepCount];
    }

    private void Update()
    {
        if (!GameManager.Instance.playerActif.enAction)
        {
            Destroy(this);
        }

        if (Mouse.current.leftButton.wasReleasedThisFrame && !GameManager.Instance.playerActif.enVisee)
        {
            Shoot();
        }

        if (Mouse.current.leftButton.isPressed)
        {
            currentMousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            velocity = ((Vector2)gameObject.transform.position - currentMousePos) * (lauchForce * 0.5f * mass);
            DrawTrajectory();
        }
        else
        {
            ClearTrajectory();
        }
    }

    private void DrawTrajectory()
    {
        Vector3[] positions = new Vector3[trajectoryStepCount];
        ClearTrajectory();

        float scale = 0.2f;
        for (int i = 1; i < trajectoryStepCount; i++)
        {
            float t = i * trajectoryTimeStep;
            Vector3 pos = transform.position + (Vector3)velocity * t +
                          (Vector3)(Physics2D.gravity * (1.2f * lauchForce * mass * t * t));// le 1.2f a changer pour la forme de la trajectoire
            positions[i] = pos;
            pointilles[i] = Instantiate(pointilleVisee, positions[i], Quaternion.identity);
            pointilles[i].transform.localScale = new Vector3(scale, scale, 1);
            scale /= 1.2f;
        }
    }

    private void ClearTrajectory()
    {
        foreach (GameObject pointille in pointilles)
        {
            if (pointille != null)
            {
                Destroy(pointille);
            }
        }
    }

    private void Aim()
    {
        // Récupérer les coordonnées de la souris en pixels
        Vector2 mousePositionPixels = Mouse.current.position.ReadValue();

        // Convertir les coordonnées de la souris de pixels à des coordonnées dans l'espace du monde
        Vector2 mousePositionWorld = Camera.main.ScreenToWorldPoint(mousePositionPixels);
        direction = (mousePositionWorld - (Vector2)transform.position).normalized;
    }

    // ReSharper disable Unity.PerformanceAnalysis
    private void Shoot()
    {
        TireurBehaviour.SpellAfterShoot(transform.position,currentMousePos);
        Destroy(this);
        if (TireurBehaviour is CarteBehaviour)
        {
            ((CarteBehaviour)TireurBehaviour).spriteRenderer.color = new Color32(200,200,200,255);
            ((CarteBehaviour)TireurBehaviour).PiocherMain();
            CarteBehaviour.alreadylifted = false;
        }
    }

    private void OnDestroy()
    {
        ClearTrajectory();
    }
}
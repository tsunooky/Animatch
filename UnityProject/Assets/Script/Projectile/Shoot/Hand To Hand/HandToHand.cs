using System;
using System.Collections;
using System.Collections.Generic;
using Destructible2D;
using UnityEngine;
using Script.Manager;
using UnityEngine.InputSystem;

public class HandToHand : MonoBehaviour
{
    public ProjectileData projectileData;
    private Vector2 direction;
    private GameObject _affichage;

    private float spawnDistance = 0.70f;

    private float delayBeforeHitBOT = 0.1f;
    public bool bot;
    private bool isAiming;
    private Transform colliderChild;

    public void Initialize(ProjectileData ProjectileData, Sprite SpriteGun)
    {
        isAiming = false;
        bot = false;
        projectileData = ProjectileData;
        if (!bot)
        {
            InvokeRepeating("Aim", 0f, 1f / 60f);
        }
    }

    void Awake()
    {
        _affichage = new GameObject("Trajectory_hand_to_hand");
        var affich = _affichage.AddComponent<SpriteRenderer>();
        Sprite sprite = Resources.Load<Sprite>("Sprites/Autre/HandToHand_Affichage");
        D2dDestructibleSprite destructibleSprite = _affichage.AddComponent<D2dDestructibleSprite>();
        affich.sprite = sprite;
        destructibleSprite.Shape = sprite;
        destructibleSprite.Rebuild();
        destructibleSprite.RebuildAlphaTex();
        destructibleSprite.Indestructible = true; // L'animal n'est pas destructible
        destructibleSprite.CropSprite = false;
        affich.enabled = false;
    }

    private void Update()
    {
        if (!GameManager.Instance.playerActif.enAction)
        {
            Destroy(this);
        }
        if (Mouse.current.leftButton.wasPressedThisFrame && GameManager.Instance.playerActif.enVisee)
        {
            SetAim(true, false);
        }
        if (Mouse.current.leftButton.wasReleasedThisFrame && isAiming && !GameManager.Instance.playerActif.enVisee)
        {
            if (!bot)
            {
                StartCoroutine(Shoot());
            }
        }

        if (Mouse.current.leftButton.isPressed && isAiming)
        {
            _affichage.transform.position = gameObject.transform.position;
            _affichage.GetComponent<SpriteRenderer>().enabled = true;
        }
        else
        {
            _affichage.GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    private void Aim()
    {
        if (!bot)
        {
            // Récupérer les coordonnées de la souris en pixels
            Vector2 mousePositionPixels = Mouse.current.position.ReadValue();

            // Convertir les coordonnées de la souris de pixels à des coordonnées dans l'espace du monde
            Vector2 mousePositionWorld = Camera.main.ScreenToWorldPoint(mousePositionPixels);
            direction = (mousePositionWorld - (Vector2)_affichage.transform.position).normalized;
            _affichage.transform.right = direction;
            _affichage.transform.rotation *= Quaternion.Euler(0, 0, -45f);
        }
    }

    private IEnumerator Shoot()
    {
        _affichage.AddComponent<D2dPolygonCollider>();
        SetAim(false, false);
        // ANIMATION BAT ICI

        // Activer le collider au moment du shoot
        AddBump();
        yield return new WaitForSeconds(delayBeforeHitBOT);
        Destroy(this);
    }

    private void OnDestroy()
    {
        Destroy(_affichage);
    }

    private void AddBump()
    {
        colliderChild = _affichage.transform.Find("Collider");
        if (colliderChild != null)
        {
            colliderChild.gameObject.AddComponent<TouchToBump>();
            Debug.Log("Le composant TouchToBump a été ajouté à " + colliderChild.name);
        }
        else
        {
            Debug.LogWarning("L'objet 'Collider' n'a pas été trouvé pour ajouter le composant TouchToBump.");
        }
    }

    private void SetAim(bool isAiming, bool aura)
    {
        GameManager.Instance.playerActif.SetAura(aura);
        this.isAiming = isAiming;
    }
}

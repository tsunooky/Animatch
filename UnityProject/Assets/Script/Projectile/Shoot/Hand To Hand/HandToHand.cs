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
    
    public bool bot;
    private bool isAiming;
    private Tireur TireurBehaviour;
    

    public void Initialize(ProjectileData ProjectileData, Tireur tireurBehaviour)
    {
        TireurBehaviour = tireurBehaviour;
        projectileData = ProjectileData;
        
        InvokeRepeating("Aim", 0f, 1f / 60f);
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
                Shoot();
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

    private void Shoot()
    {
        // Récupérer les coordonnées de la souris en pixels
        Vector2 mousePositionPixels = Mouse.current.position.ReadValue();
        TireurBehaviour.SpellAfterShoot(transform.position, mousePositionPixels);
        SetAim(false, false);
        Destroy(this);
    }

    private void OnDestroy()
    {
        Destroy(_affichage);
    }
    
    private void SetAim(bool isAiming, bool aura)
    {
        GameManager.Instance.playerActif.SetAura(aura);
        this.isAiming = isAiming;
    }
}

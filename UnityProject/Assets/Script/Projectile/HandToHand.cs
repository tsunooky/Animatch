using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Script.Manager;
using UnityEngine.InputSystem;

public class HandToHand : MonoBehaviour
{
    public ProjectileData projectileData;
    private Vector2 direction;
    private GameObject _affichage;

    private float spawnDistance = 0.70f;

    private float delayBeforeHitBOT = 5f;
    public bool bot;
    private bool isAiming;

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
        _affichage = new GameObject("Trajectory_hand_tot_hand");
        var affich = _affichage.AddComponent<SpriteRenderer>();
        affich.sprite = Resources.Load<Sprite>("Sprites/Autre/HandToHand_Affichage");
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
            setAim(true,false);
        }
        if (Mouse.current.leftButton.wasReleasedThisFrame && isAiming && !GameManager.Instance.playerActif.enVisee)
        {
            if (!bot)
            {
                StartCoroutine(Shoot());
            }
        }

        if (Input.GetMouseButton(0) && isAiming)
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
        }
    }

    // ReSharper disable Unity.PerformanceAnalysis
    private IEnumerator Shoot()
    {
        setAim(false, false);
        //ANIMATION BAT ICI
        _affichage.AddComponent<TouchToBump>();
        yield return new WaitForSeconds(delayBeforeHitBOT);
        Destroy(this);
    }
    
    private void OnDestroy()
    {
        Destroy(_affichage);
    }
    
    
    private void setAim(bool isAiming, bool aura)
    {
        GameManager.Instance.playerActif.SetAura(aura);
        this.isAiming = isAiming;
    }
    
}

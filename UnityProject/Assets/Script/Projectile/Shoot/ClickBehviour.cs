using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks.Sources;
using Script.Manager;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using Script.Manager;


public class ClickBehaviour : MonoBehaviour
{
    private GameObject gun;
    private Tireur Tireur;
    

    public void Initialize(Tireur tireur)
    {
        Tireur = tireur;
    }

    private void Update()
    {
        if (!GameManager.Instance.playerActif.enAction)
        {
            Destroy(gun);
            Destroy(this);
        }
        else if (Mouse.current.leftButton.wasPressedThisFrame) 
        {
            if(GameManager.Instance.playerActif.enVisee)
            {
               Tireur.SpellAfterClick();
               Destroy(this);
            }
        }
    }
}
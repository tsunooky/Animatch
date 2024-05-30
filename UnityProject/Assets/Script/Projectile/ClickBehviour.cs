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
    private CarteBehaviour carteBehaviour;
    

    public void Initialize(CarteBehaviour CarteBehaviour)
    {
        carteBehaviour = CarteBehaviour;
    }

    private void Update()
    {
        if (!GameManager.Instance.playerActif.enAction)
        {
            Destroy(gun);
            Destroy(this);
        }
        else if (Mouse.current.leftButton.wasPressedThisFrame && GameManager.Instance.playerActif.enVisee)
        {
           carteBehaviour.SpellAfterClick();
        }
    }
}
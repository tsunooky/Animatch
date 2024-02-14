using System;
using System.Collections;
using System.Collections.Generic;
using Script.Manager;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Experimental.Rendering.RenderGraphModule;
using static Script.Manager.GameManager;

public abstract class CarteBehaviour : MonoBehaviour
{
    public SpriteRenderer spriteRenderer; 

    public PlayerManager player;

    public CarteData carteData;

    protected abstract void Awake();
    
    private void Start()
    {
        spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
        gameObject.AddComponent<BoxCollider2D>().size = new Vector2(1.75f,3);
        spriteRenderer.sprite = carteData.Sprite;
        spriteRenderer.sortingOrder = 1;
        player = Instance.joueur;
    }

    private void OnMouseDown()
    {
        if (Instance.playerActif == player && player.drops - carteData.drops >= 0 && !player.enAction)
        { 
            Instance.playerActif.enAction = true;
            Spell();
            player.drops -= carteData.drops;
        }
        else if (player.enAction)
        {
            Instance.playerActif.enAction = false;
        }
    }

    private void OnMouseEnter()
    {
        var vector3 = transform.position;
        vector3.y += 1f;
        transform.position = vector3;
    }

    private void OnMouseExit()
    {
        var vector3 = transform.position;
        vector3.y =  -5f;
        transform.position = vector3;
    }

    protected abstract void Spell();

    protected virtual void RemoveSpell()
    {
        Instance.playerActif.enAction = false;
    }
}

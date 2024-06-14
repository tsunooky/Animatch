using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Script.Manager;

public class BoutonAnimax : MonoBehaviour
{
    
    private SpriteRenderer spriteRenderer;
    
    private void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }
    
    private void OnMouseDown()
    {
        AnimalBehaviour animalActif = GameManager.Instance.playerActif.animalActif;
        if (GameManager.Instance.playerActif == GameManager.Instance.joueur)
        {
            GameManager.Instance.playerActif.enAction = true;
            animalActif.Animax();
        }
    }
    
    void Update()
    {
        AnimalBehaviour animalActif = GameManager.Instance.playerActif.animalActif;
        if (animalActif is not null)
            spriteRenderer.sprite = animalActif.animalData.Animax;
    }
}

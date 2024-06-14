using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Script.Manager;
using Unity.VisualScripting;

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
        if (GameManager.Instance.playerActif == GameManager.Instance.joueur 
            && spriteRenderer.sprite is not null && !animalActif.AnimaxActivate)
        {
            spriteRenderer.color = Color.grey;
            GameManager.Instance.playerActif.enAction = true;
            animalActif.AnimaxActivate = true;
            animalActif.Animax();
        }
    }
    
    void Update()
    {
        AnimalBehaviour animalActif = GameManager.Instance.playerActif.animalActif;
        if (animalActif is not null)
            spriteRenderer.sprite = animalActif.animalData.Animax;

        if (!animalActif.AnimaxActivate)
        {
            spriteRenderer.color = Color.white;
        }
        else
        {
            spriteRenderer.color = Color.grey;
        }
    }
}

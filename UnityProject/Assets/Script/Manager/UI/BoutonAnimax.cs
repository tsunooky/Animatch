using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Script.Manager;
using Unity.VisualScripting;
using static Script.Manager.AGameManager;

public class BoutonAnimax : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    
    private void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }
    
    private void OnMouseDown()
    {
        if (Instance is GameManager gameManager)
        {
            AnimalBehaviour animalActif = GameManager.Instance.playerActif.animalActif;
            if (Instance.playerActif == GameManager.Instance.joueur 
                && spriteRenderer.sprite is not null && !animalActif.AnimaxActivate)
            {
                spriteRenderer.color = Color.grey;
                GameManager.Instance.playerActif.enAction = true;
                animalActif.AnimaxActivate = true;
                animalActif.Animax();
            }    
        }
        if (Instance is GameManager2J gameManager2)
        {
            AnimalBehaviour animalActif = GameManager.Instance.playerActif.animalActif;
            if (Instance.playerActif == GameManager.Instance.joueur 
                && spriteRenderer.sprite is not null && !animalActif.AnimaxActivate)
            {
                spriteRenderer.color = Color.grey;
                GameManager.Instance.playerActif.enAction = true;
                animalActif.AnimaxActivate = true;
                animalActif.Animax();
            }
            else if (gameManager2.playerActif == gameManager2.joueur2
                     && spriteRenderer.sprite is not null && !animalActif.AnimaxActivate)
            {
                spriteRenderer.color = Color.grey;
                GameManager.Instance.playerActif.enAction = true;
                animalActif.AnimaxActivate = true;
                animalActif.Animax();    
            }
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

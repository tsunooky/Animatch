using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Script.Manager;
using Unity.VisualScripting;
using static Script.Manager.AGameManager;
using UnityEngine.UI;

public class BoutonAnimax : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public GameObject AffichText;
    
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
        {
            spriteRenderer.sprite = animalActif.animalData.Animax;
            Text txt = AffichText.GetComponent<Text>();
            txt.text = animalActif.DefAnimax;
        }

        if (!animalActif.AnimaxActivate)
        {
            spriteRenderer.color = Color.white;
        }
        else
        {
            spriteRenderer.color = Color.grey;
        }
    }
    private void OnMouseEnter()
    {
        AnimalBehaviour animalActif = GameManager.Instance.playerActif.animalActif;
        if (!animalActif.AnimaxActivate)
        {
            StartCoroutine(OnMouseEnter2());
        }
    }

    private IEnumerator OnMouseEnter2()
    {
        yield return new WaitForSeconds(0.5f);
        AffichText.SetActive(true); 
    }
    private void OnMouseExit()
    {
        StartCoroutine(OnMouseExit2());
    }
    private IEnumerator OnMouseExit2()
    {
        yield return new WaitForSeconds(0.5f);
        AffichText.SetActive(false);   
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Script.Manager;
using Unity.VisualScripting;
using static Script.Manager.AGameManager;
using UnityEngine.UI;
public class BoutonPassif : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public GameObject AffichText;
    
    private void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        AnimalBehaviour animalActif = GameManager.Instance.playerActif.animalActif;
        if (animalActif is not null)
        {
            spriteRenderer.sprite = animalActif.animalData.Passif;
            Text txt = AffichText.GetComponent<Text>();
            txt.text = animalActif.DefPassive;
        }
    }
    
    private void OnMouseEnter()
    {
        StartCoroutine(OnMouseEnter2());
    }

    private IEnumerator OnMouseEnter2()
    {
        yield return new WaitForSeconds(0.5f);
        AffichText.SetActive(true); // Affiche le texte lorsque la souris entre    
    }
    private void OnMouseExit()
    {
        StartCoroutine(OnMouseExit2());
    }
    private IEnumerator OnMouseExit2()
    {
        yield return new WaitForSeconds(0.5f);
        AffichText.SetActive(false); // Affiche le texte lorsque la souris entre    
    }
}
using UnityEngine;
using Script.Manager;

public class BoutonPassif : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    
    private void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        AnimalBehaviour animalActif = GameManager.Instance.playerActif.animalActif;
        if (animalActif is not null)
            spriteRenderer.sprite = animalActif.animalData.Passif;
    }
}
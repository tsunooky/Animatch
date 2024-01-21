using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalController : MonoBehaviour
{

    public AnimalBehaviour animalBehaviour;
    public SpriteRenderer spriteRenderer;
    
    public float spriteScaleFactor = 0.2f;
    void Awake()
    {
        gameObject.AddComponent<Rigidbody2D>();
        spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
        animalBehaviour = gameObject.AddComponent<TortueBehaviour>();
    }

    void Start()
    {
        animalBehaviour.animalData = Resources.Load<AnimalData>("Data/Tortue");
        
        // Affecte le sprite au composant SpriteRenderer
        spriteRenderer.sprite = Resources.Load<Sprite>("Sprites/Animaux/turtle");
        
        spriteRenderer.transform.localScale = new Vector3(spriteScaleFactor, spriteScaleFactor, 1.0f);
        gameObject.AddComponent<CircleCollider2D>();
    }
}

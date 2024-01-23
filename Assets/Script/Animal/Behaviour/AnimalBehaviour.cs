using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AnimalBehaviour : MonoBehaviour
{
    public AnimalData animalData;
    
    // ReSharper disable Unity.PerformanceAnalysis
    public void AnimalVisible()
    {
        gameObject.AddComponent<Rigidbody2D>();
        SpriteRenderer spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
        spriteRenderer.sortingOrder = 1;
        spriteRenderer.sprite = animalData.sprite;
        spriteRenderer.transform.localScale = new Vector3(0.2f, 0.2f, 1.0f);
        gameObject.AddComponent<CircleCollider2D>();
        gameObject.AddComponent<CameraManager>();
    }
    
    public abstract void LancerPouvoir(GameObject gameObject);
}

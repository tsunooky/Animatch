using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Destructible2D.Examples;

public class BulletBehaviour : MonoBehaviour
{
    [SerializeField] private float speed = 5f;

    private Rigidbody2D rb;

    public GameObject Prefab;
    
    public delegate void BulletDestroyed();
    public static event BulletDestroyed OnBulletDestroyed;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (FindObjectsOfType<D2dExplosion>().Length == 0)
        {       
            var clone = Instantiate(Prefab, transform.position, transform.rotation);
            clone.SetActive(true);
            OnBulletDestroyed?.Invoke();
            Destroy(gameObject);
        }
    }
    
    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        SetStraightVelocity();
    }

    private void SetStraightVelocity()
    {
        rb.velocity = transform.right * speed;
    }
    
}
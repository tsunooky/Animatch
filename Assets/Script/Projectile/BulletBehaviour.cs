using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Destructible2D.Examples;

public class BulletBehaviour : MonoBehaviour
{
    private Rigidbody2D rb;

    public GameObject Prefab;

    public ProjectileData projectileData;
    
    public delegate void BulletDestroyed();
    public static event BulletDestroyed OnBulletDestroyed;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (FindObjectsOfType<D2dExplosion>().Length == 0)
        {       
            var clone = Instantiate(Prefab, transform.position, transform.rotation);
            clone.SetActive(true);
            clone.GetComponent<D2dExplosion>().degat = projectileData.Degat;
            Destroy(gameObject);
        }
    }
    
    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        SetStraightVelocity();
        OnBulletDestroyed?.Invoke();
    }

    public void SetPrefab(GameObject prefab)
    {
        Prefab = prefab;
    }

    private void SetStraightVelocity()
    {
        rb.velocity = transform.right * projectileData.Force;
    }
    
}
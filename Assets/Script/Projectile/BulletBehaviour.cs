using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Destructible2D.Examples;

public class BulletBehaviour : MonoBehaviour
{

    [SerializeField] private float speed = 5f;

    private Rigidbody2D rb;
    public GameObject Prefab;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        SetStraightVelocity();
    }

    // Update is called once per frame
    private void SetStraightVelocity()
    {
        rb.velocity = transform.right * speed;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (FindObjectsOfType<D2dExplosion>().Length == 0)
        {       
            var clone = Instantiate(Prefab, transform.position, transform.rotation);
            clone.SetActive(true);
            Destroy(gameObject);
        }
    }
}
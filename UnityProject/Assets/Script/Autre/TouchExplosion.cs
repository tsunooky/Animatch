using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchExplosion : MonoBehaviour
{
    public ProjectileData projectileData;
    private List<Collision2D> listBump;
    
    void Start()
    {
        listBump = new List<Collision2D>();
        gameObject.AddComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        listBump.Add(other);
        Debug.Log(("a"));
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        listBump.Remove(other);
        Debug.Log("a");
    }

    private void OnDestroy()
    {
        foreach (Collision2D other in listBump)
        {
            other.gameObject.GetComponent<Rigidbody2D>().velocity = gameObject.transform.right * projectileData.Force * 0.075f;
            Debug.Log(other.gameObject.name);
        }
        Debug.Log("a");
    }
}

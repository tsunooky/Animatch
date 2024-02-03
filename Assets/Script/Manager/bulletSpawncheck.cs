using System.Collections.Generic;
using System;
using UnityEngine;


public class bulletSpawnCheck : MonoBehaviour
{
    public GameObject animal;
    
    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Map")
        {
            var vector2 = animal.gameObject.transform.position;
            vector2.y = 10;
            animal.gameObject.transform.position = vector2;   
        }
    }

    private void Update()
    {
        if (animal != null)
            gameObject.transform.position = animal.transform.position;
    }
}

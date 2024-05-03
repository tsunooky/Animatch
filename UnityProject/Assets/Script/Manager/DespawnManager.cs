using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespawnManager : MonoBehaviour
{
    private void Update()
    {
        if (gameObject.transform.position.y < -5)
        {
            Destroy(gameObject);
        }
    }
}

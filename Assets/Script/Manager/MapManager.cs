using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    List<Collision2D> check = new List<Collision2D>();
    public void OnCollisionExit2D(Collision2D other)
    {
        if (!check.Contains(other))
        {
            var vector2 = other.gameObject.transform.position;
            vector2.y = 10;
            other.gameObject.transform.position = vector2;
        }
    }

    public void OnCollisionStay2D(Collision2D other)
    {
        check.Add(other);
    }
}

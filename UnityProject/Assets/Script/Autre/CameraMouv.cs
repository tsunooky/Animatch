using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMouv : MonoBehaviour
{ 
    public float dragSpeed = 12f;
    private Vector3 dragOrigin;

    // Définir les limites de la caméra
    public float leftLimit = -10.0f;
    public float rightLimit = 10.0f;
    public float topLimit = 4.0f;
    public float bottomLimit = -4.0f;

    void Update()
    {
        if (Input.GetMouseButtonDown(1)) 
        {
            dragOrigin = Input.mousePosition;
            return;
        }

        if (!Input.GetMouseButton(1)) return; 

        Vector3 pos = Camera.main.ScreenToViewportPoint(dragOrigin - Input.mousePosition);
        Vector3 move = new Vector3(pos.x * dragSpeed, pos.y * dragSpeed, 0);

        transform.Translate(move, Space.World);

        // Limiter le mouvement de la caméra à la scène
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, leftLimit, rightLimit), Mathf.Clamp(transform.position.y, bottomLimit, topLimit),transform.position.z);

        dragOrigin = Input.mousePosition; // Mettez à jour l'origine du glissement pour le prochain frame
    }
}



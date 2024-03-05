using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ProjectilesBehaviour : CarteBehaviour
{
    protected void Start()
    {
        gameObject.AddComponent<Photon.Pun.PhotonTransformView>();
        gameObject.AddComponent<Photon.Pun.PhotonRigidbodyView>();
    }
    
}

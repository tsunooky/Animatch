using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalBehaviour : MonoBehaviour
{
    public AnimalData animalData;

    public AnimalBehaviour(AnimalData animaldata)
    {
        animalData = animaldata;
    }
    
    public virtual void LancerPouvoir(GameObject gameObject)
    {
        Debug.Log($"{animalData.nom} est en : ({gameObject.transform.position.x}," +
                  $"{gameObject.transform.position.y})");
    }
}

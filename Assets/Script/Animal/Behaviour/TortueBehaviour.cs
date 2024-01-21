using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TortueBehaviour : AnimalBehaviour
{
    public TortueBehaviour(AnimalData animaldata):base(animaldata){}
    public override void LancerPouvoir(GameObject gameObject)
    {
        Debug.Log("Je suis une tres belle tortue qui danse la salsa");
    }
}

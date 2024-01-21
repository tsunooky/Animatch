using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NouveauAnimal", menuName = "Animaux")]
public class AnimalData : ScriptableObject
{
   public string nom;
   public Sprite sprite;
   public int Pv;
   public int force;
   public float poids; 
   public float vitesse;
}

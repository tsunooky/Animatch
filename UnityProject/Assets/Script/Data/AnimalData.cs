using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NouveauAnimal", menuName = "Animal")]
public class AnimalData : ScriptableObject
{
   public string nom;
   public Sprite sprite;
   public int Pv;
   public float Poids;
   public int Vitesse;
}

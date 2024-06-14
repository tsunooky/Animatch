using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NouveauAnimal", menuName = "Animal")]
public class AnimalData : ScriptableObject
{
   public Sprite sprite;
   public int Pv;
   public Sprite Animax;
   public Sprite Passif;
   public Sprite Clignement;
}

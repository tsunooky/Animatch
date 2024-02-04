using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NouvelleCarte", menuName = "Carte")]
public class CarteData : ScriptableObject
{
    public ProjectileData projectileData;
    public Sprite Sprite;
    public int drops;
    public string type;
}

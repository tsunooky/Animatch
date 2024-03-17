using Script.Manager;
using UnityEngine;
using UnityEngine.Rendering.UI;

public class BatBehaviour : CarteBehaviour
{
    protected override void Awake()
    {
        carteData = Resources.Load<CarteData>("Data/Carte/Bat");
    }

    protected override void Spell()
    {
        GameManager.Instance.playerActif.animalActif.gameObject.AddComponent<HandToHand>().Initialize(carteData.projectileData, 
            carteData.projectileData.SpriteIfHandToHand);
    }
}
using Script.Manager;
using UnityEngine;

public class TomateBehaviour : CarteBehaviour
{
    protected override void Awake()
    {
        carteData = Resources.Load<CarteData>("Data/Carte/Tomate");
    }

    protected override void Spell()
    {
        GameManager.Instance.playerActif.animalActif.gameObject.AddComponent<AimAndShoot>().Initialize(carteData.projectileData,
            carteData.projectileData.Projectile.GetComponent<Sprite>());
    }
}

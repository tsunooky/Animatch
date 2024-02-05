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
        if (GameManager.Instance.animalActif != null)
        {
            GameManager.Instance.animalActif.gameObject.AddComponent<AimAndShoot>().Initialize("tomate");
        }
    }
}

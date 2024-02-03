using UnityEngine;

public class TomateBehaviour : CarteBehaviour
{
    protected override void Awake()
    {
        carteData = Resources.Load<CarteData>("Data/Carte/Tomate");
    }

    protected override void Spell()
    {
        if (player.animalActif.gameObject != null)
        {player.animalActif.gameObject.AddComponent<AimAndShoot>().Initialize("tomate");}
    }
}

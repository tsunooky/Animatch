using Script.Manager;
using UnityEngine;

public class BombeBehaviour : CarteBehaviour
{
    protected override void Awake()
    {
        carteData = Resources.Load<CarteData>("Data/Carte/Bombe");
    }

    protected override void SpellClickOnCarte()
    {
        GameManager.Instance.playerActif.animalActif.gameObject.AddComponent<ClickBehaviour>().Initialize(this);
    }
    
    public override void SpellAfterClick()
    {
        GameManager.Instance.playerActif.animalActif.gameObject.AddComponent<AimBehviour>().Initialize(carteData.projectileData,this);
    }

    public override void SpellAfterShoot(Vector2 startPosition ,Vector2 currentMousePos)
    {
        alreadylifted = false;
        ClassiqueShoot(startPosition,currentMousePos);
    }
}
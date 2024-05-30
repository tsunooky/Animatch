using System;
using Script.Manager;
using UnityEngine;
using UnityEngine.Rendering.UI;

public class HealBehaviour : CarteBehaviour
{
    protected override void Awake()
    {
        carteData = Resources.Load<CarteData>("Data/Carte/Heal");
    }

    protected override void SpellClickOnCarte()
    {
        GameManager.Instance.playerActif.animalActif.gameObject.AddComponent<ClickBehaviour>().Initialize(this);
    }
    
    public override void SpellAfterClick()
    {
        GameManager.Instance.playerActif.animalActif.pv += 10;
    }

    public override void SpellAfterShoot(Vector2 startPosition,Vector2 currentMousePos)
    {
        // Cette carte n'a pas besoin de cette fonction
        throw new NotImplementedException();
    }


}
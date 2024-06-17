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
        var vector3 = transform.position;
        vector3.y =  -4f;
        transform.position = vector3;
        spriteRenderer.color = new Color32(200,200,200,255);
        GameManager.Instance.playerActif.animalActif.pv += 20;
        GameManager.Instance.playerActif.animalActif.healthBar.SetHealth(GameManager.Instance.playerActif.animalActif.pv);
        PiocherMain();
        FinAction();
    }

    public override void SpellAfterShoot(Vector2 startPosition,Vector2 currentMousePos)
    {
        // Cette carte n'a pas besoin de cette fonction
        throw new NotImplementedException();
    }

    
}
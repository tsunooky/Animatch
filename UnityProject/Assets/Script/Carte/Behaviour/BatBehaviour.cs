using Script.Manager;
using UnityEngine;
using UnityEngine.Rendering.UI;

public class BatBehaviour : CarteBehaviour
{
    protected override void Awake()
    {
        carteData = Resources.Load<CarteData>("Data/Carte/Bat");
    }

    protected override void SpellClickOnCarte()
    {
        GameManager.Instance.playerActif.animalActif.gameObject.AddComponent<ClickBehaviour>();
    }
    
    public override void SpellAfterClick()
    {
        GameManager.Instance.playerActif.animalActif.gameObject.AddComponent<AimBehviour>();
    }

    public override void SpellAfterShoot(Vector2 startPosition,Vector2 currentMousePos)
    {
        // A CHANGER POUR ADAPTER AU CORPS à CORPS
       ClassiqueShoot(startPosition,currentMousePos);
    }
    
}
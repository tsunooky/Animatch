using Script.Manager;
using UnityEngine;
using UnityEngine.Rendering.UI;

public class JumpBehaviour : CarteBehaviour
{
    private int Bump; 
    
    protected override void Awake()
    {
        // TEMPORAIRE OU DEFINITIF EN FONCTION (Bump)
        Bump = 24;
        carteData = Resources.Load<CarteData>("Data/Carte/Jump");
    }

    protected override void SpellClickOnCarte()
    {
        GameManager.Instance.playerActif.animalActif.gameObject.AddComponent<ClickBehaviour>().Initialize(this);
    }
    
    public override void SpellAfterClick()
    {
        GameManager.Instance.playerActif.animalActif.gameObject.AddComponent<AimBehviour>().Initialize(this);
    }

    public override void SpellAfterShoot(Vector2 startPosition,Vector2 currentMousePos)
    {
        GameManager.Instance.playerActif.animalActif.gameObject.GetComponent<Rigidbody2D>().velocity = (startPosition - currentMousePos) * (Bump * 0.075f);
        CarteBehaviour.alreadylifted = false;
        FinAction();
    }

    
}
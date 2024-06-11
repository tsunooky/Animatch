using Script.Manager;
using UnityEngine;
using System.Collections;

public class KnifeBehaviour : CarteBehaviour
{
    protected override void Awake()
    {
        carteData = Resources.Load<CarteData>("Data/Carte/knife");
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
        StartCoroutine(ShootCoroutine(startPosition, currentMousePos));
    }

    

    private IEnumerator ShootCoroutine(Vector2 startPosition, Vector2 currentMousePos)
    {
        for (int i = 0; i < 4; i++)
        {
            ClassiqueShoot(startPosition, currentMousePos);
            // Attendre pendant le délai spécifié
            yield return new WaitForSeconds((float)1.5);
        }
    }

    
}
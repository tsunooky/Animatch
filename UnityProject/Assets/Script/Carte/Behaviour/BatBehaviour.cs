using System.Collections;
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
        GameManager.Instance.playerActif.animalActif.gameObject.AddComponent<ClickBehaviour>().Initialize(this);
    }
    
    public override void SpellAfterClick()
    {
        GameManager.Instance.playerActif.animalActif.gameObject.AddComponent<HandToHand>().Initialize(carteData.projectileData, this);
    }

    public override void SpellAfterShoot(Vector2 startPosition,Vector2 currentMousePos)
    {
        StartCoroutine(CoupBat(startPosition,currentMousePos));
    }

    private IEnumerator CoupBat(Vector2 startPosition,Vector2 currentMousePos)
    {
        Debug.Log("JOSSELIN EST LE GOAT");
        //Création bat

        GameObject bat = Instantiate(carteData.projectileData.Projectile,startPosition,Quaternion.identity);
        bat.AddComponent<TouchToBump>();
        
        //animation Bat
        
        // A FAIRE
        yield return new WaitForSeconds(1);
        
        //Fin anim
        Destroy(bat);
        
        FinAction();
    }
    
}
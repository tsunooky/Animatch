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
        StartCoroutine(CoupBat(startPosition));
    }
    private IEnumerator CoupBat(Vector2 startPosition)
    {
        
        GameObject pivot = new GameObject("BatPivot");
        pivot.transform.position = startPosition;

        
        GameObject bat = Instantiate(carteData.projectileData.Projectile, pivot.transform.position, Quaternion.identity);
        bat.AddComponent<TouchToBump>();

        
        SpriteRenderer batSpriteRenderer = bat.GetComponent<SpriteRenderer>();
        Vector2 batSize = batSpriteRenderer.bounds.size;

        
        bat.transform.SetParent(pivot.transform);
        bat.transform.localPosition = new Vector3(0, batSize.y / 2, 0); 

        
        float duration = 0.5f;
        float elapsedTime = 0.0f;

        
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;

            
            float currentAngle = Mathf.Lerp(0, 360, t);

           
            pivot.transform.rotation = Quaternion.Euler(0, 0, currentAngle);

            yield return null;
        }

        
        Destroy(bat);
        Destroy(pivot);

        FinAction();
        PiocherMain();
    }











}
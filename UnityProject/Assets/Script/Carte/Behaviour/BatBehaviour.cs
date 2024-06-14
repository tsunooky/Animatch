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

    private IEnumerator CoupBat(Vector2 startPosition, Vector2 currentMousePos)
    {
        // Création bat
        GameObject bat = Instantiate(carteData.projectileData.Projectile, startPosition, Quaternion.identity);
        bat.AddComponent<TouchToBump>();
    
        // Calcul de la direction vers laquelle la batte doit pointer
        Vector2 direction = currentMousePos - startPosition;
        float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
    
        // Définir l'angle de départ et de fin pour l'animation
        float startAngle = targetAngle + 45f;  // 45 degrés en haut
        float endAngle = targetAngle - 45f;    // 45 degrés en bas
    
        // Durée de l'animation en secondes
        float duration = 1.0f;
        float elapsedTime = 0.0f;
    
        // Animation Bat
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;
    
            // Interpolation de l'angle de départ à l'angle de fin
            float currentAngle = Mathf.Lerp(startAngle, endAngle, t);
    
            // Appliquer la rotation à la batte
            bat.transform.rotation = Quaternion.Euler(0, 0, currentAngle);
    
            yield return null;
        }
    
        // Fin anim
        Destroy(bat);
    
        FinAction();
    }
    
}
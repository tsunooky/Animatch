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
        // Création d'un objet pivot pour ajuster le pivot de rotation de la batte
        GameObject pivot = new GameObject("BatPivot");
        pivot.transform.position = startPosition;

        // Création de la batte
        GameObject bat = Instantiate(carteData.projectileData.Projectile, pivot.transform.position, Quaternion.identity);
        bat.AddComponent<TouchToBump>();

        // Ajuster le pivot de la batte pour qu'il soit au niveau du manche
        SpriteRenderer batSpriteRenderer = bat.GetComponent<SpriteRenderer>();
        Vector2 batSize = batSpriteRenderer.bounds.size;

        // Positionner la batte de manière à ce que son manche soit au pivot
        bat.transform.SetParent(pivot.transform);
        bat.transform.localPosition = new Vector3(0, batSize.y / 2, 0); // Déplacer pour que le manche soit à la position de départ

        // Calcul de la direction vers laquelle la batte doit pointer
        Vector2 direction = currentMousePos - startPosition;
        float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Vérification des coordonnées d'entrée
        Debug.Log($"Start Position: {startPosition}, Current Mouse Position: {currentMousePos}, Direction: {direction}, Target Angle: {targetAngle}");

        // Définir l'angle de départ et de fin pour l'animation
        float startAngle = targetAngle + 25f;  // angle entre start et current plus 25 degrés
        float endAngle = targetAngle - 25f;    // angle entre start et current moins 25 degrés

        // Durée de l'animation en secondes
        float duration = 0.5f;
        float elapsedTime = 0.0f;

        // Animation de la rotation de la batte
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;

            // Interpolation de l'angle de départ à l'angle de fin
            float currentAngle = Mathf.Lerp(startAngle, endAngle, t);

            // Appliquer la rotation à l'objet pivot
            pivot.transform.rotation = Quaternion.Euler(0, 0, currentAngle);

            yield return null;
        }

        // Fin de l'animation : détruire la batte et le pivot
        Destroy(bat);
        Destroy(pivot);

        // Fin de l'action (à adapter selon votre besoin)
        FinAction();
        PiocherMain();  // Exemple d'une autre fonction appelée après le coup de batte
    }






}
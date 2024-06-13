using UnityEngine;




public  class AimBot : MonoBehaviour
{
    public void ClassiqueShootbot(Vector2 targetPosition)
    {
        var pro = Resources.Load<ProjectileData>("Data/Projectile/Tomate");
        
        GameObject bullet = Instantiate(pro.Projectile, gameObject.transform.position, Quaternion.identity);
        ProjectileBehaviour bulletBehaviour = bullet.GetComponent<ProjectileBehaviour>();
        if (gameObject.transform.position.x < 0 )
        {
            bulletBehaviour.Set((new Vector2(-gameObject.transform.position.x, gameObject.transform.position.y), targetPosition), pro);
        }
        else
        {
            bulletBehaviour.Set((new Vector2(gameObject.transform.position.x, gameObject.transform.position.y), targetPosition), pro);
        }
    }
    
    public void TirerDansUneDirectiondroite( Vector2 cible)
    {
        var pro = Resources.Load<ProjectileData>("Data/Projectile/Tomate");
        GameObject bullet = Instantiate(pro.Projectile, gameObject.transform.position, Quaternion.identity);
        pro.Degat += 10;
        // Obtenir le composant ProjectileBehaviour
        ProjectileBehaviour bulletBehaviour = bullet.GetComponent<ProjectileBehaviour>();

        // Calculer la position relative de la cible par rapport au bot
        Vector2 botPosition = gameObject.transform.position;
        Vector2 relativePosition = cible - botPosition;

        // Générer un décalage aléatoire
        float randomOffsetX = UnityEngine.Random.Range(0.5f, 1f);
        float randomOffsetY = UnityEngine.Random.Range(0.5f, 1f);
        
        // Ajuster la direction pour tirer à droite avec un décalage aléatoire
        Vector2 direction = new Vector2(relativePosition.x + 1f + randomOffsetX, relativePosition.y + 3f + randomOffsetY);

        // Convertir la direction relative en une position absolue pour la cible
        Vector2 targetPosition = botPosition + direction;

        // Définir la vélocité du projectile
        bulletBehaviour.SetDirection(targetPosition, pro);
        
    }
    public void TirerDansUneDirectiongauche( Vector2 cible)
    {
        var pro = Resources.Load<ProjectileData>("Data/Projectile/Tomate");
        GameObject bullet = Instantiate(pro.Projectile, gameObject.transform.position, Quaternion.identity);
        pro.Degat += 10;
        // Obtenir le composant ProjectileBehaviour
        ProjectileBehaviour bulletBehaviour = bullet.GetComponent<ProjectileBehaviour>();

        // Calculer la position relative de la cible par rapport au bot
        Vector2 botPosition = gameObject.transform.position;
        Vector2 relativePosition = cible - botPosition;

        
        float randomOffsetX = UnityEngine.Random.Range(-1f, -0.5f);
        float randomOffsetY = UnityEngine.Random.Range(0.5f, 1f);
        
        // Ajuster la direction pour tirer à gauche avec un décalage aléatoire
        Vector2 direction = new Vector2(relativePosition.x - 1f + randomOffsetX, relativePosition.y + 3f + randomOffsetY );

        // Convertir la direction relative en une position absolue pour la cible
        Vector2 targetPosition = botPosition + direction;

        // Définir la vélocité du projectile
        bulletBehaviour.SetDirection(targetPosition, pro);
        
    }
}
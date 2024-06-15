using UnityEngine;




public class AimBot : MonoBehaviour
{

    void Start()
    {
        // Assurez-vous que le projectile n'entre pas en collision avec le bot au départ
        Collider2D botCollider = transform.root.GetComponent<Collider2D>();
        Collider2D projectileCollider = GetComponent<Collider2D>();

        if (botCollider != null && projectileCollider != null)
        {
            Physics2D.IgnoreCollision(projectileCollider, botCollider, true);
        }
    }

    public void ClassiqueShootbot(Vector2 targetPosition)
    {
        var pro = Resources.Load<ProjectileData>("Data/Projectile/Tomate");

        GameObject bullet = Instantiate(pro.Projectile, gameObject.transform.position, Quaternion.identity);
        ProjectileBehaviour bulletBehaviour = bullet.GetComponent<ProjectileBehaviour>();
        if (gameObject.transform.position.x < 0)
        {
            bulletBehaviour.Set(
                (new Vector2(-gameObject.transform.position.x, gameObject.transform.position.y), targetPosition), pro);
        }
        else
        {
            bulletBehaviour.Set(
                (new Vector2(gameObject.transform.position.x, gameObject.transform.position.y), targetPosition), pro);
        }
    }

    public void TirerDansUneDirectiondroite(Vector2 cible)
    {
        Debug.Log("tirer dans une direction droite");
        var pro = Resources.Load<ProjectileData>("Data/Projectile/Tomate");

        // Décalage initial pour éviter les collisions immédiates
        Vector2 initialOffset = new Vector2(0.5f, 0.5f);
        Vector2 initialPosition = (Vector2)transform.position + initialOffset;

        GameObject bullet = Instantiate(pro.Projectile, initialPosition, Quaternion.identity);
        ProjectileBehaviour bulletBehaviour = bullet.GetComponent<ProjectileBehaviour>();

        Vector2 botPosition = transform.position;
        Vector2 relativePosition = cible - botPosition;

        // Vérifier si la cible est en dessous
        if (relativePosition.y < -1.5f)
        {
            Debug.Log("Il tire pas dans les pieds");
            relativePosition = new Vector2(5, 0.1f);
        }

        Vector2 direction = new Vector2(relativePosition.x, relativePosition.y);
        Vector2 targetPosition = botPosition + direction;

        // Vérifier si la cible est loin et ajuster la vélocité
        float distance = relativePosition.magnitude;
        if (distance > 3.0f) // Seuil de distance pour augmenter la vélocité
        {
            float velocityMultiplier = distance / 2.5f; // Ajuster ce facteur pour contrôler la vélocité
            bulletBehaviour.SetDirection(targetPosition, pro, velocityMultiplier);
        }
        else
        {
            bulletBehaviour.SetDirection(targetPosition, pro);
        }
    }

    public void TirerDansUneDirectiongauche(Vector2 cible)
    {
        Debug.Log("tirer dans une direction gauche");
        var pro = Resources.Load<ProjectileData>("Data/Projectile/Tomate");

        // Décalage initial pour éviter les collisions immédiates
        Vector2 initialOffset = new Vector2(-0.5f, 0.5f);
        Vector2 initialPosition = (Vector2)transform.position + initialOffset;

        GameObject bullet = Instantiate(pro.Projectile, initialPosition, Quaternion.identity);
        ProjectileBehaviour bulletBehaviour = bullet.GetComponent<ProjectileBehaviour>();

        Vector2 botPosition = transform.position;
        Vector2 relativePosition = cible - botPosition;

        // Vérifier si la cible est en dessous
        if (relativePosition.y < -1.5f)
        {
            Debug.Log("Il tire pas dans les pieds");
            relativePosition = new Vector2(-5, 0.1f);
        }

        Vector2 direction = new Vector2(relativePosition.x, relativePosition.y);
        Vector2 targetPosition = botPosition + direction;

        // Vérifier si la cible est loin et ajuster la vélocité
        float distance = relativePosition.magnitude;
        if (distance > 3f) // Seuil de distance pour augmenter la vélocité
        {
            float velocityMultiplier = distance / 2.5f; // Ajuster ce facteur pour contrôler la vélocité
            bulletBehaviour.SetDirection(targetPosition, pro, velocityMultiplier);
        }
        else
        {
            bulletBehaviour.SetDirection(targetPosition, pro);
        }
    }
}

using UnityEngine;




public class AimBot : MonoBehaviour
{

    void Start()
    {
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

        
        Vector2 initialOffset = new Vector2(0.5f, 0.5f);
        Vector2 initialPosition = (Vector2)transform.position + initialOffset;

        GameObject bullet = Instantiate(pro.Projectile, initialPosition, Quaternion.identity);
        ProjectileBehaviour bulletBehaviour = bullet.GetComponent<ProjectileBehaviour>();

        Vector2 botPosition = transform.position;
        Vector2 relativePosition = cible - botPosition;

        
        if (relativePosition.y < -1.5f)
        {
            Debug.Log("Il tire pas dans les pieds");
            relativePosition = new Vector2(5, 0.1f);
        }

        Vector2 direction = new Vector2(relativePosition.x, relativePosition.y);
        Vector2 targetPosition = botPosition + direction;

        
        float distance = relativePosition.magnitude;
        if (distance > 3.0f)
        {
            float velocityMultiplier = distance / 3f; 
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

        
        Vector2 initialOffset = new Vector2(-0.5f, 0.5f);
        Vector2 initialPosition = (Vector2)transform.position + initialOffset;

        GameObject bullet = Instantiate(pro.Projectile, initialPosition, Quaternion.identity);
        ProjectileBehaviour bulletBehaviour = bullet.GetComponent<ProjectileBehaviour>();

        Vector2 botPosition = transform.position;
        Vector2 relativePosition = cible - botPosition;

        
        if (relativePosition.y < -1.5f)
        {
            Debug.Log("Il tire pas dans les pieds");
            relativePosition = new Vector2(-5, 0.1f);
        }

        Vector2 direction = new Vector2(relativePosition.x, relativePosition.y);
        Vector2 targetPosition = botPosition + direction;

       
        float distance = relativePosition.magnitude;
        if (distance > 3f) 
        {
            float velocityMultiplier = distance / 3f; 
            bulletBehaviour.SetDirection(targetPosition, pro, velocityMultiplier);
        }
        else
        {
            bulletBehaviour.SetDirection(targetPosition, pro);
        }
    }
}

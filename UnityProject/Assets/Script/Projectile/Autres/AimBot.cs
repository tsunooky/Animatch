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
}
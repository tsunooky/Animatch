using UnityEngine;




public  class aimBot : MonoBehaviour
{
    public void ClassiqueShootbot(Vector2 targetPosition)
    {
        var pro = Resources.Load<ProjectileData>("Data/Projectile/Tomate");
        GameObject bullet = Instantiate(pro.Projectile, gameObject.transform.position, Quaternion.identity);
        ProjectileBehaviour bulletBehaviour = bullet.GetComponent<ProjectileBehaviour>();
        bulletBehaviour.Set((gameObject.transform.position,targetPosition), pro);
    }
}
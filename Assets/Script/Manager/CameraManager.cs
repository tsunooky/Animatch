using UnityEngine;

namespace Script.Manager
{
    public class CameraManager : MonoBehaviour
    {
        // Cette fonction est appelée lorsque le GameObject devient invisible pour la caméra
        private void OnBecameInvisible()
        {
            // Détruire le GameObject actuel
            Destroy(gameObject);
        }
    }
}

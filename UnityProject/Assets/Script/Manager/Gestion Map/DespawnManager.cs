using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Destructible2D;
using UnityEngine;

public class DespawnManager : MonoBehaviour
{
    private bool isDestroying = false;

    private void Update()
    {
        if (!isDestroying && gameObject.transform.position.y < -4.3f)
        {
            if (gameObject.GetComponent<AnimalBehaviour>() is null)
                Destroy(gameObject);
            else
            {
                gameObject.GetComponent<AnimalBehaviour>().Degat(10000);
            }
        }
    }

    public IEnumerator Death()
    {
        Destroy(gameObject.GetComponent<Rigidbody2D>());
        Destroy(gameObject.GetComponent<D2DPollygonCollider>());
        
        isDestroying = true;
        
        float duration = 1.0f;
        float elapsedTime = 0.0f;
        Vector3 originalScale = transform.localScale;
        Vector3 targetScale = originalScale * 4; 
        float initialRotation = transform.rotation.eulerAngles.z;
        float targetRotation = initialRotation + 360; 

        while (elapsedTime < duration)
        {
            transform.localScale = Vector3.Lerp(originalScale, targetScale, elapsedTime / duration);

            float currentRotation = Mathf.Lerp(initialRotation, targetRotation, elapsedTime / duration);
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, currentRotation));

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.localScale = targetScale;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, targetRotation));

        Destroy(gameObject);
    }
}

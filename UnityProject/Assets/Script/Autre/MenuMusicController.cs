using UnityEngine;

public class MenuMusicController : MonoBehaviour
{
    public AudioSource audioSource; // Assurez-vous que ce champ soit assigné dans l'inspecteur

    void Start()
    {
        if (audioSource != null)
        {
            audioSource.Play(); // Démarre la musique au début de la scène
        }
    }
    void Awake()
    {
        DontDestroyOnLoad(gameObject); // Empêche la destruction de l'objet lors du chargement d'une nouvelle scène
    }
}
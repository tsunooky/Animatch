using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuMusicController : MonoBehaviour
{
    public AudioSource audioSource;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        if (audioSource != null)
        {
            audioSource.Play();
        }

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("MenuMusicController - Scene loaded: " + scene.name);

        if (scene.name == "Main")
        {
            // Arrêter et détruire le MenuMusicController quand on entre dans la scène de jeu
            if (audioSource != null && audioSource.isPlaying)
            {
                audioSource.Stop();
                
            }

            // Détruire cet objet pour permettre au GameMusicController de gérer la musique
            Destroy(gameObject);
        }
        else
        {
            if (audioSource != null && !audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
    }
}
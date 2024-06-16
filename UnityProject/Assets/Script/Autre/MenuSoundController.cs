using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuSoundController : MonoBehaviour
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
            
            if (audioSource != null && audioSource.isPlaying)
            {
                audioSource.Stop();
                
            }

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
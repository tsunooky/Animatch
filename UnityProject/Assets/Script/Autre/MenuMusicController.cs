using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuMusicController : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioSource audioSource2; // AudioSource pour jouer les musiques
    public AudioClip[] gameMusicClips; // Tableau des musiques pour la scène de jeu

    private int lastClipIndex = -1;

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
        
        if (scene.name == "Main")
        {
            audioSource.Stop();
            PlayRandomMusic();
        }
        
    }
    private void PlayRandomMusic()
    {

        int randomIndex;
        do
        {
            randomIndex = Random.Range(0, gameMusicClips.Length);
        } while (randomIndex == lastClipIndex && gameMusicClips.Length > 1);

        lastClipIndex = randomIndex;

        audioSource2.clip = gameMusicClips[randomIndex];
        Debug.Log("Playing random music: " + audioSource2.clip.name); // Pour le débogage
        audioSource2.Play();
    }
}
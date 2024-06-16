using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuMusicController : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioSource audioSource2; 
    public AudioClip[] gameMusicClips; 
    public AudioClip MenuMusicClip;
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
        
        if (scene.name == "Main" || scene.name == "Main 2P" )
        {
            audioSource.Stop();
            PlayRandomMusic();
        }
        
        if (scene.name == "Fin")
        {
            audioSource.Stop();
            PlayMusicMenu();
        }
        
        
    }

    private void PlayMusicMenu()
    {
        audioSource.clip = MenuMusicClip;
        audioSource.Play();
    }
    
    private void PlayRandomMusic()
    {

        int randomIndex;
        do
        {
            randomIndex = Random.Range(0, gameMusicClips.Length);
        } while (randomIndex == lastClipIndex && gameMusicClips.Length > 1);

        lastClipIndex = randomIndex;

        audioSource.clip = gameMusicClips[randomIndex];
        Debug.Log("Playing random music: " + audioSource.clip.name); 
        audioSource.Play();
    }
}
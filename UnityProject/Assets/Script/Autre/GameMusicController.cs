using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMusicController : MonoBehaviour
{
    public static GameMusicController instance; 
    public AudioSource audioSource2;
    public AudioClip[] gameMusicClips;

    private int lastClipIndex = -1; 

    void Awake()
    { 
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); 
            PlayRandomMusic(); 
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void PlayRandomMusic()
    {
        if (gameMusicClips.Length == 0 || audioSource2 == null)
        {
            Debug.LogWarning("Aucune musique à jouer ou AudioSource non assignée.");
            return;
        }

        int randomIndex;
        do
        {
            randomIndex = Random.Range(0, gameMusicClips.Length);
        } while (randomIndex == lastClipIndex && gameMusicClips.Length > 1);

        lastClipIndex = randomIndex;

        audioSource2.clip = gameMusicClips[randomIndex];
        Debug.Log("Playing random music: " + audioSource2.clip.name); 
        audioSource2.Play();
    }
}
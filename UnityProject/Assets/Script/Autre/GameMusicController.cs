using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMusicController : MonoBehaviour
{
    public static GameMusicController instance; // Instance statique pour le singleton
    public AudioSource audioSource2; // AudioSource pour jouer les musiques
    public AudioClip[] gameMusicClips; // Tableau des musiques pour la scène de jeu

    private int lastClipIndex = -1; // Pour éviter de jouer la même musique deux fois de suite (optionnel)

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
            // Si une autre instance existe déjà, détruire celle-ci
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
        Debug.Log("Playing random music: " + audioSource2.clip.name); // Pour le débogage
        audioSource2.Play();
    }
}
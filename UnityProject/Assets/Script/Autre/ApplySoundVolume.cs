using UnityEngine;

public class ApplySoundVolume : MonoBehaviour
{
    // Clé PlayerPrefs pour stocker le volume des sons
    private const string SoundVolumePrefKey = "SoundVolume";

    private void Start()
    {
        // Récupérer le volume enregistré
        float savedVolume = PlayerPrefs.GetFloat(SoundVolumePrefKey, 0.3f);

        // Mettre à jour le volume des sources audio dans la scène
        UpdateAudioSourcesVolume(savedVolume);
    }

    private void UpdateAudioSourcesVolume(float volume)
    {
        // Trouver tous les AudioSource de la scène actuelle et mettre à jour leur volume
        AudioSource[] audioSources = FindObjectsOfType<AudioSource>();
        foreach (AudioSource source in audioSources)
        {
            // Ignorer la source audio de la musique du menu
            if (source.CompareTag("2DSource"))
            {
                source.volume = volume;
            }
        }
    }
}
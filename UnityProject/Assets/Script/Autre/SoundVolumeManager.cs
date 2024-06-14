using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SoundVolumeManager : MonoBehaviour
{
    public Slider soundSlider; // Slider pour contrôler le volume des sons
    public TMP_Text volumePercentageText; // Texte pour afficher le pourcentage de volume

    // Clé PlayerPrefs pour stocker le volume des sons
    private const string SoundVolumePrefKey = "SoundVolume";

    private void Start()
    {
        // Initialiser le slider avec le volume actuel enregistré
        float savedVolume = PlayerPrefs.GetFloat(SoundVolumePrefKey, 0.3f);
        soundSlider.value = savedVolume;
        soundSlider.onValueChanged.AddListener(AdjustSoundVolume);
        UpdateVolumePercentageText(savedVolume);
    }

    public void AdjustSoundVolume(float newVolume)
    {
        // Mettre à jour le volume dans PlayerPrefs
        PlayerPrefs.SetFloat(SoundVolumePrefKey, newVolume);
        PlayerPrefs.Save();

        // Mettre à jour le volume des sources audio dans la scène
        UpdateAudioSourcesVolume(newVolume);
        UpdateVolumePercentageText(newVolume);
    }

    private void UpdateVolumePercentageText(float volume)
    {
        if (volumePercentageText != null)
        {
            int percentage = Mathf.RoundToInt(volume * 100); // Convertir le volume en pourcentage
            volumePercentageText.text = percentage.ToString() + "%";
        }
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
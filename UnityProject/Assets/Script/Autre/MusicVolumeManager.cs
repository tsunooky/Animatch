using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MusicVolumeManager : MonoBehaviour
{
    public Slider musicSlider; // Référence au Slider
    public TMP_Text volumePercentageText; // Référence au texte qui affiche le pourcentage

    private AudioSource menuMusicSource;

    private void Start()
    {
        // Trouver l'objet MenuMusic qui n'a pas été détruit lors du changement de scène
        GameObject menuMusic = GameObject.Find("MenuMusic");
        if (menuMusic != null)
        {
            menuMusicSource = menuMusic.GetComponent<AudioSource>();
        }

        // Initialiser le slider avec le volume actuel de la musique
        if (musicSlider != null && menuMusicSource != null)
        {
            musicSlider.value = menuMusicSource.volume;
            musicSlider.onValueChanged.AddListener(AdjustVolume);
            UpdateVolumePercentageText(musicSlider.value);
        }
    }

    public void AdjustVolume(float newVolume)
    {
        if (menuMusicSource != null)
        {
            menuMusicSource.volume = newVolume;
            UpdateVolumePercentageText(newVolume);
        }
    }

    private void UpdateVolumePercentageText(float volume)
    {
        if (volumePercentageText != null)
        {
            int percentage = Mathf.RoundToInt(volume * 100); // Convertit le volume en pourcentage
            volumePercentageText.text = percentage.ToString() + "%";
        }
    }
}


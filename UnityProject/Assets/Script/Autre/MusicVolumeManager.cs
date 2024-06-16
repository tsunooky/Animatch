using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MusicVolumeManager : MonoBehaviour
{
    public Slider musicSlider; 
    public TMP_Text volumePercentageText; 

    private AudioSource menuMusicSource;
    private AudioSource gameMenuSource;

    private void Start()
    {
        
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
            int percentage = Mathf.RoundToInt(volume * 100); 
            volumePercentageText.text = percentage.ToString() + "%";
        }
    }
}


using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SoundVolumeManager : MonoBehaviour
{
    public Slider soundSlider; 
    public TMP_Text volumePercentageText; 

    
    private const string SoundVolumePrefKey = "SoundVolume";

    private void Start()
    {
      
        float savedVolume = PlayerPrefs.GetFloat(SoundVolumePrefKey, 0.3f);
        soundSlider.value = savedVolume;
        soundSlider.onValueChanged.AddListener(AdjustSoundVolume);
        UpdateVolumePercentageText(savedVolume);
    }

    public void AdjustSoundVolume(float newVolume)
    {
        
        PlayerPrefs.SetFloat(SoundVolumePrefKey, newVolume);
        PlayerPrefs.Save();

        UpdateAudioSourcesVolume(newVolume);
        UpdateVolumePercentageText(newVolume);
    }

    private void UpdateVolumePercentageText(float volume)
    {
        if (volumePercentageText != null)
        {
            int percentage = Mathf.RoundToInt(volume * 100); 
            volumePercentageText.text = percentage.ToString() + "%";
        }
    }

    private void UpdateAudioSourcesVolume(float volume)
    {
        AudioSource[] audioSources = FindObjectsOfType<AudioSource>();
        foreach (AudioSource source in audioSources)
        {
            
            if (source.CompareTag("2DSource"))
            {
                source.volume = volume;
            }
        }
    }
}
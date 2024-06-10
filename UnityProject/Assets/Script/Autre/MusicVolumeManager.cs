using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class NewBehaviourScript : MonoBehaviour
{
public AudioSource audioSource; // Assignez votre Audio Source ici
    public Slider volumeSlider; // Assignez votre Slider ici
    public TMP_Text volumePercentageText;

    void Start()
    {
        // Charger le volume de la musique à partir des préférences sauvegardées
        float savedVolume = PlayerPrefs.GetFloat("MusicVolume", 0.50f); // Par défaut, le volume est à 100%
        audioSource.volume = savedVolume;

        // Si un Slider est assigné, synchroniser son état
        if (volumeSlider != null)
        {
            volumeSlider.value = savedVolume;
            volumeSlider.onValueChanged.AddListener(delegate { AdjustVolume(volumeSlider.value); });
        }
        UpdateVolumePercentageText(savedVolume);
    }

    public void AdjustVolume(float newVolume)
    {
        audioSource.volume = newVolume; // Ajuste le volume de l'audio source
        PlayerPrefs.SetFloat("MusicVolume", newVolume); // Sauvegarder la nouvelle valeur de volume
        PlayerPrefs.Save();
        UpdateVolumePercentageText(newVolume);
    }
    private void UpdateVolumePercentageText(float volume)
    {
        if (volumePercentageText != null)
        {
            int percentage = Mathf.RoundToInt(volume * 100); // Convertit le volume en pourcentage
            volumePercentageText.text = percentage.ToString() + "%";
        }
    }
    
    void Awake()
    {
        DontDestroyOnLoad(gameObject); // Empêche la destruction de l'objet lors du chargement d'une nouvelle scène
    }
}

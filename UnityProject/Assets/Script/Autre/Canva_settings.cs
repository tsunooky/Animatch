using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // Utilisation de UnityEngine.UI pour Button et Slider

public class CanvasSettings : MonoBehaviour
{
    
    public Button settingsButton;  
    public GameObject feuillage;   
    public TextMeshProUGUI musicText; 
    public Slider musicSlider;    
    public TextMeshProUGUI percentageText;
    public Button Menu;
    public GameObject Selection; 
    private bool areSettingsOpen = false; 

    void Start()
    {
        
        settingsButton.interactable = true;
        Menu.interactable = false;
        Menu.gameObject.SetActive(false);
        feuillage.SetActive(false);
        Selection.SetActive(false);
        musicText.gameObject.SetActive(false);
        musicSlider.gameObject.SetActive(false);
        percentageText.gameObject.SetActive(false);

       
        settingsButton.onClick.AddListener(ToggleSettings);
    }

    // Méthode pour ouvrir ou fermer les paramètres
    public void ToggleSettings()
    {
        areSettingsOpen = !areSettingsOpen; 
        if (areSettingsOpen)
        {
            // Ouvrir les paramètres
            Menu.interactable = true;
            Menu.gameObject.SetActive(true);
            feuillage.SetActive(true);
            Selection.SetActive(true);
            musicText.gameObject.SetActive(true);
            musicSlider.gameObject.SetActive(true);
            percentageText.gameObject.SetActive(true);
        }
        else
        {
            // Fermer les paramètres
            Menu.interactable = false;
            Menu.gameObject.SetActive(false);
            feuillage.SetActive(false);
            Selection.SetActive(false);
            musicText.gameObject.SetActive(false);
            musicSlider.gameObject.SetActive(false);
            percentageText.gameObject.SetActive(false);
        }
    }
    
    public void GoToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // Utilisation de UnityEngine.UI pour Button et Slider

public class CanvasSettings : MonoBehaviour
{
    // Assure-toi que ces variables sont assignées dans l'Inspecteur
    public Button settingsButton;  // Bouton pour ouvrir/fermer les paramètres
    public GameObject feuillage;   // Le fond des paramètres
    public TextMeshProUGUI musicText; // TMP pour afficher "Music"
    public Slider musicSlider;     // Slider pour ajuster le volume
    public TextMeshProUGUI percentageText;
    public Button Menu;// TMP pour afficher le pourcentage du volume
    public GameObject Selection; 
    private bool areSettingsOpen = false; // État des paramètres (ouverts ou fermés)

    void Start()
    {
        // Initialisation de l'état des éléments UI
        settingsButton.interactable = true;
        Menu.interactable = false;
        Menu.gameObject.SetActive(false);
        feuillage.SetActive(false);
        Selection.SetActive(false);
        musicText.gameObject.SetActive(false);
        musicSlider.gameObject.SetActive(false);
        percentageText.gameObject.SetActive(false);

        // Ajout du listener pour le bouton settings
        settingsButton.onClick.AddListener(ToggleSettings);
    }

    // Méthode pour ouvrir ou fermer les paramètres
    public void ToggleSettings()
    {
        areSettingsOpen = !areSettingsOpen; // Bascule l'état des paramètres

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
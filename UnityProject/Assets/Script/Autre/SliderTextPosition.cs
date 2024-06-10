using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SliderTextPosition : MonoBehaviour
{
    public Slider volumeSlider; // Référence au Slider
    public TMP_Text volumePercentageText; // Référence au texte qui affiche le pourcentage
    public RectTransform textRectTransform; // Référence au RectTransform du texte pour le déplacer

    void Start()
    {
        if (volumeSlider != null)
        {
            // S'abonner à l'événement onValueChanged du Slider
            volumeSlider.onValueChanged.AddListener(delegate { UpdateTextPosition(volumeSlider.value); });

            // Initialiser la position du texte en fonction de la valeur initiale du Slider
            UpdateTextPosition(volumeSlider.value);
        }
    }

    void UpdateTextPosition(float sliderValue)
    {
        // Convertir la valeur du Slider en pourcentage pour le texte
        int percentage = Mathf.RoundToInt(sliderValue * 100);
        volumePercentageText.text = percentage.ToString() + "%";

        // Calculer la position du curseur du Slider
        RectTransform sliderRectTransform = volumeSlider.GetComponent<RectTransform>();
        Vector3[] sliderWorldCorners = new Vector3[4];
        sliderRectTransform.GetWorldCorners(sliderWorldCorners);

        // Calculer la position locale du curseur sur le Slider
        float normalizedValue = sliderValue - volumeSlider.minValue;
        normalizedValue = normalizedValue / (volumeSlider.maxValue - volumeSlider.minValue);

        // Calculer la position sur l'axe X du texte en fonction de la position du curseur
        float cursorX = Mathf.Lerp(sliderWorldCorners[0].x, sliderWorldCorners[2].x, normalizedValue);

        // Mettre à jour la position du texte
        Vector3 textPosition = new Vector3(cursorX, textRectTransform.position.y, textRectTransform.position.z);
        textRectTransform.position = textPosition;
    }
}
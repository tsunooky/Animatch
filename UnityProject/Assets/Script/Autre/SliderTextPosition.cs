using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SliderTextPosition : MonoBehaviour
{
    public Slider volumeSlider; 
    public TMP_Text volumePercentageText; 
    public RectTransform textRectTransform; 

    void Start()
    {
        if (volumeSlider != null)
        {
            
            volumeSlider.onValueChanged.AddListener(delegate { UpdateTextPosition(volumeSlider.value); });

            
            UpdateTextPosition(volumeSlider.value);
        }
    }

    void UpdateTextPosition(float sliderValue)
    {
        
        int percentage = Mathf.RoundToInt(sliderValue * 100);
        volumePercentageText.text = percentage.ToString() + "%";

        
        RectTransform sliderRectTransform = volumeSlider.GetComponent<RectTransform>();
        Vector3[] sliderWorldCorners = new Vector3[4];
        sliderRectTransform.GetWorldCorners(sliderWorldCorners);

        
        float normalizedValue = sliderValue - volumeSlider.minValue;
        normalizedValue = normalizedValue / (volumeSlider.maxValue - volumeSlider.minValue);

        
        float cursorX = Mathf.Lerp(sliderWorldCorners[0].x, sliderWorldCorners[2].x, normalizedValue);

       
        Vector3 textPosition = new Vector3(cursorX, textRectTransform.position.y, textRectTransform.position.z);
        textRectTransform.position = textPosition;
    }
}
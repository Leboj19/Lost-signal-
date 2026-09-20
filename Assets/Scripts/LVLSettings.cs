using UnityEngine;
using UnityEngine.UI;

public class LVLSettings : MonoBehaviour
{
    [Header("Volume")]
    public Slider volumeSlider;

    [Header("Brightness")]
    public Slider brightnessSlider;
    public Image brightnessOverlay;

    void Start()
    {
        float savedVolume = PlayerPrefs.GetFloat("volume", 1f);
        float savedBrightness = PlayerPrefs.GetFloat("brightness", 1f);

        volumeSlider.value = savedVolume;
        brightnessSlider.value = savedBrightness;

        SetVolume(savedVolume);
        SetBrightness(savedBrightness);
    }

    public void SetVolume(float value)
    {
        AudioListener.volume = value;
        PlayerPrefs.SetFloat("volume", value);
    }

    public void SetBrightness(float value)
    {
        Color c = brightnessOverlay.color;
        c.a = 1f - value;
        brightnessOverlay.color = c;
        PlayerPrefs.SetFloat("brightness", value);
    }
}
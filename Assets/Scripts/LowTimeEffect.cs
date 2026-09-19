using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class LowTimeEffect : MonoBehaviour
{
    public Timer timerScript; // Reference to the Timer script
    public Volume volume; // Volume component
    private Vignette vignette; // Vignette effect
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        volume.profile.TryGet(out vignette);
    }

    // Update is called once per frame
    void Update()
    {
        float timerValue = timerScript.timer; // Get the current timer value from the Timer script
        if (timerValue > 30f)
        {
            vignette.intensity.value = 0f; // No vignette effect
        }
        else if (timerValue > 20f)
        {
            vignette.intensity.value = 0.2f; 
        }
        else if (timerValue > 10f)
        {
            vignette.intensity.value = 0.4f; 
        }
        else if (timerValue > 5f)
        {
            vignette.intensity.value = 0.6f; 
        }
        else if (timerValue > 0f)
        {
            vignette.intensity.value = 0.8f; 
        }
        else
        {
            vignette.intensity.value = 1f;
        }
        
    }
}

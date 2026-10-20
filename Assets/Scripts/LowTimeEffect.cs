using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class LowTimeEffect : MonoBehaviour
{
    public Timer timerScript; // Reference to the Timer script
    public Volume volume; // Volume component
    private Vignette vignette; // Vignette effect
    private ColorAdjustments colorAdjustments;

    void Start()
    {
        volume.profile.TryGet(out vignette);

        volume.profile.TryGet(out colorAdjustments);
    }
    // Update is called once per frame
    void Update()
    {
        float timerValue = timerScript.timer; // Get the current timer value from the Timer script
        if (timerValue > 120f)
        {
            vignette.intensity.value = 0f; // No vignette effect
        }
        else if (timerValue > 90f)
        {
            vignette.intensity.value = 0.15f;
        }
        else if (timerValue > 60f)
        {
            vignette.intensity.value = 0.3f;
        }
        else if (timerValue > 30f)
        {
            vignette.intensity.value = 0.5f;
        }
        else if (timerValue > 10f)
        {
            vignette.intensity.value = 0.7f;
        }
        else
        {
            vignette.intensity.value = 0.9f;
        }
        if (timerValue > 120f)
        { // More than 2 minutes // Normal screen
          colorAdjustments.colorFilter.value = Color.white; } 
        
        else if (timerValue > 90f) 
        { 
            //90-120 seconds 
            // Slight red
            colorAdjustments.colorFilter.value = new Color(1f, 0.9f, 0.9f);
         
        } else if (timerValue > 60f) 
        { 
            // 60-90 seconds // Noticeable red
            colorAdjustments.colorFilter.value = new Color(1f, 0.75f, 0.75f); 
        }
        else if (timerValue > 30f) 
        { 
            // 30-60 seconds // Strong red
            colorAdjustments.colorFilter.value = new Color(1f, 0.55f, 0.55f); 
        }
        else if (timerValue > 10f) { // 10-30 seconds // Very strong red
       colorAdjustments.colorFilter.value = new Color(1f, 0.3f, 0.3f); 
        } 
        else 
        { 
        // 0-10 seconds // Maximum red
        colorAdjustments.colorFilter.value = new Color(1f, 0.1f, 0.1f); 
        
        } 
    }

  

}

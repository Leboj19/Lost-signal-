using TMPro;
using UnityEngine;
using System.Collections;

public class CommunicationRoomController : MonoBehaviour
{
    [Header("Screen")]
    public GameObject communicationScreen;
    public GameObject screenPrompt;

    [Header("Screen Text")]
    public TMP_Text screenText;

    [Header("Room Lights")]
    public Light[] roomLights;

    [Header("Communication Audio")]
    
    public AudioSource communicationAudio;
    // System ready: Mixkit, "Cybernetic technology affirmation"
    // Source: https://mixkit.co/free-sound-effects/technology/

    public AudioClip systemReadySound;
    
    // Signal restoring: Pixabay, "Futuristic UI Beeps"
    // Source: https://pixabay.com/sound-effects/film-special-effects-futuristic-ui-beeps-356129/
    
    public AudioClip signalRestoringSound;
    
    // Blackout: Pixabay, electric shutdown sound effects
    // Source: https://pixabay.com/sound-effects/search/electric-shutdown/
    public AudioClip blackoutSound;
    
    // Power restored: Mixkit, "Electronics power up"
    // Source: https://mixkit.co/free-sound-effects/technology/
    public AudioClip powerRestoredSound;
    
    // Communication complete: Mixkit, "High tech bleep confirmation"
    // Source: https://mixkit.co/free-sound-effects/technology/
    public AudioClip communicationCompleteSound;

    private bool systemReady = false;
    private bool sequenceStarted = false;

    void Start()
    {
        //Screen = off at first.
        if (communicationScreen != null)
        {
            communicationScreen.SetActive(false);
        }

        if (screenPrompt != null)
        {
            screenPrompt.SetActive(false);
        }
    }
    public void SystemReady()
    {
        if (systemReady)
            return;

        systemReady = true;

        Debug.Log("COMMUNICATION SYSTEM READY!");

        // Turns on the screen.
        if (communicationScreen != null)
        {
            communicationScreen.SetActive(true);
        }

        // Shows the interaction prompt.
        if (screenPrompt != null)
        {
            screenPrompt.SetActive(true);
            PlaySound(systemReadySound);
        }
    }
    public void StartCommunication()
    {
        if (!systemReady)
        {
            Debug.Log("Communication system is not ready.");
            return;
        }

        if (sequenceStarted)
            return;

        sequenceStarted = true;

        StartCoroutine(RestorationSequence());
    }
    private IEnumerator RestorationSequence()
    {
        if (screenText != null)
        {
            screenText.text = "SIGNAL INITIALISING...";
        }

        yield return new WaitForSeconds(2f);
        
        if (screenText != null)
        {
            screenText.text = "RESTORING SIGNAL....";
        }

        yield return new WaitForSeconds(2f);

        if (screenText != null)
        {
            screenText.text = "RESTORING SIGNAL...\n\n[███░░░░░░░]";
        }

        yield return new WaitForSeconds(1f);

        if (screenText != null)
        {
            screenText.text = "RESTORING SIGNAL...\n\n[██████░░░░]";
        }

        yield return new WaitForSeconds(1f);

        if (screenText != null)
        {
            screenText.text = "RESTORING SIGNAL...\n\n[██████████]";
        }

        yield return new WaitForSeconds(2f);

        if (screenText != null)
        {
            screenText.text = "SIGNAL RESTORED";
        }

        Debug.Log("SIGNAL RESTORED!");

        yield return new WaitForSeconds(2f);

        PlaySound(blackoutSound);
        TurnLightsOff();

        Debug.Log("BLACKOUT!");

        yield return new WaitForSeconds(2f);

        PlaySound(powerRestoredSound);
        TurnLightsOn();

        Debug.Log("SYSTEM RESTORED!");

        if (screenText != null)
        {
            screenText.text = "SIGNAL RESTORED\n\nCOMMUNICATION LINK ESTABLISHED";
            PlaySound(communicationCompleteSound);
        }

        yield return new WaitForSeconds(3f);

        SetScreenText("INCOMING TRANSMISSION...\n\n" +
            "MAIN SHIP: SIGNAL RECEIVED.\n" +
            "WE HAVE YOUR LOCATION.\n\n" +
            "PREPARE FOR EXTRACTION.");

        Debug.Log("LEVEL COMPLETE!");

        Timer timer = FindFirstObjectByType<Timer>();

        if (timer != null)
        {
            timer.GameCompleted();
        }
        else
        {
            Debug.LogWarning("Timer not found!");
        }
     
}
    private void SetScreenText(string message)
    {
        if (screenText != null)
            screenText.text = message;
    }

    private void PlaySound(AudioClip clip)
    {
        if(communicationAudio != null && clip != null)
        {
            communicationAudio.PlayOneShot(clip);
        }
    }

    private void TurnLightsOff()
    {
        if (roomLights == null)
            return;

        foreach (Light light in roomLights)
        {
            if (light != null)
            {
                light.enabled = false;
            }
        }
    }

    private void TurnLightsOn()
    {
        if (roomLights == null)
            return;

        foreach (Light light in roomLights)
        {
            if (light != null)
            {
                light.enabled = true;
            }
        }
    }
}

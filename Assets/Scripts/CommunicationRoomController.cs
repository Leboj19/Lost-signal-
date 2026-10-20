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

        TurnLightsOff();

        Debug.Log("BLACKOUT!");

        yield return new WaitForSeconds(2f);

        TurnLightsOn();

        Debug.Log("SYSTEM RESTORED!");

        if (screenText != null)
        {
            screenText.text = "SIGNAL RESTORED\n\nCOMMUNICATION LINK ESTABLISHED";
        }

        yield return new WaitForSeconds(3f);

        Debug.Log("LEVEL COMPLETE!");
    }

    private void TurnLightsOff()
    {
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
        foreach (Light light in roomLights)
        {
            if (light != null)
            {
                light.enabled = true;
            }
        }
    }
}

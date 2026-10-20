using System.Collections;
using UnityEngine;
using TMPro;

public class WelcomeText : MonoBehaviour
{
    [Header("UI Elements")]
    public TextMeshProUGUI messageText; 
    
    [Header("Timing Settings")]
    public float timeBetweenCharacters = 0.05f; 
    public float visibleDuration = 3f;          

            

    private void Start()
    {
    
        ShowMessage(@"RS-Q2 SYSTEM ONLINE.
MOTHER SHIP SIGNAL STATUS: LOST

WELCOME ABOARD.");
    }

    public void ShowMessage(string message)
    {
        StartCoroutine(TypewriterRoutine(message));
    }

    private IEnumerator TypewriterRoutine(string message)
    {

        messageText.text = "";
        messageText.gameObject.SetActive(true);

   
        foreach (char letter in message.ToCharArray())
        {
            messageText.text += letter;

            yield return new WaitForSeconds(timeBetweenCharacters);
        }

        yield return new WaitForSeconds(visibleDuration);

        messageText.gameObject.SetActive(false);
    }
}

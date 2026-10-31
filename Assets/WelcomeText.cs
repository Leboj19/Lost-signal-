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

    private Coroutine typingCoroutine;



    private void Start()
    {
    
        ShowMessage(@"WELCOME ABOARD,<color=red><b>RS-Q2</b></color> SYSTEM ONLINE.
MOTHER SHIP SIGNAL STATUS: LOST");
    }

    public void ShowMessage(string message)
    {
        if(typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }
        
        typingCoroutine = StartCoroutine(TypewriterRoutine(message));
    }

    private IEnumerator TypewriterRoutine(string message)
    {
        if (messageText == null)
        {
            Debug.LogWarning("WelcomeText: Message Text is not assigned!");
            yield break;
        }
       
        messageText.gameObject.SetActive(true);

        messageText.text = message;
        messageText.maxVisibleCharacters = 0;

        messageText.ForceMeshUpdate();

        int totalCharacters = messageText.textInfo.characterCount;


        for (int i =0; i< totalCharacters; i++)
        {
            messageText.maxVisibleCharacters = i + 1;

            yield return new WaitForSeconds(timeBetweenCharacters);
        }

        yield return new WaitForSeconds(visibleDuration);

        messageText.gameObject.SetActive(false);
        messageText.maxVisibleCharacters = int.MaxValue;
        typingCoroutine = null;
    }

}

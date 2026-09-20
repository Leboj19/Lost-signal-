using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.InputSystem;

public class NarrationScript : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    public float typingSpeed = 0.04f;
    public FadeScript fadeScript; 
    [TextArea(3, 10)]
    public string[] lines;

    private int currentLine = 0;

    void Start()
    {
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        dialogueText.text = "";
        foreach (char letter in lines[currentLine].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            NextLine();
        }
    }

    void NextLine()
    {
        if (currentLine < lines.Length - 1)
        {
            currentLine++;
            StopAllCoroutines();
            StartCoroutine(TypeLine());
        }
        else
        {
            fadeScript.StartFade();
        }
    }
}
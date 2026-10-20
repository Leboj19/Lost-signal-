using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.InputSystem;

public class NarrationScript : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    public float typingSpeed = 0.04f;
    public StaticScript StaticScript;
    [TextArea(3, 10)]
    public string[] lines;

    private int currentLine = 0;
    private bool isTyping = false;
    private bool finished = false;
    private Coroutine typingCoroutine;

    void Start()
    {
        StartTyping();
    }

    void Update()
    {
        if (finished) return;

        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            if (isTyping)
                CompleteLine();
            else
                NextLine();
        }
    }

    void StartTyping()
    {
        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);

        typingCoroutine = StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        isTyping = true;
        dialogueText.text = "";

        foreach (char letter in lines[currentLine])
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;
        typingCoroutine = null;
    }

    void CompleteLine()
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
            typingCoroutine = null;
        }

        dialogueText.text = lines[currentLine];
        isTyping = false;
    }

    void NextLine()
    {
        if (currentLine < lines.Length - 1)
        {
            currentLine++;
            StartTyping();
        }
        else
        {
            finished = true;
            dialogueText.gameObject.SetActive(false);
            if (StaticScript != null)
               StaticScript.Show();
        }
    }
}
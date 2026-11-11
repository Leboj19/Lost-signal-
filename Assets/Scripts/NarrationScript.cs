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
        if (lines == null || lines.Length == 0)
        {
            Debug.LogWarning("NarrationScript: no lines assigned.");
            finished = true;
            return;
        }

        dialogueText.richText = true;
        StartTyping();
    }

    void Update()
    {
        if (finished) return;
        if (Keyboard.current == null) return;

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

        // Put the whole line in at once, but show none of it yet.
        // TextMeshPro reads the <color> tags itself and hides them.
        dialogueText.text = lines[currentLine];
        dialogueText.maxVisibleCharacters = 0;
        dialogueText.ForceMeshUpdate();

        // characterCount only counts visible characters, not tags
        int total = dialogueText.textInfo.characterCount;

        for (int i = 1; i <= total; i++)
        {
            dialogueText.maxVisibleCharacters = i;
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

        dialogueText.maxVisibleCharacters = int.MaxValue;   // show the whole line
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
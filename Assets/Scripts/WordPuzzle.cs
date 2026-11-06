using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System.Collections;

public class WordPuzzle : MonoBehaviour
{
    [Header("Puzzle UI")]
    public GameObject puzzleCanvas;
    public TMP_InputField inputField;
    public TMP_Text feedbackText;
    public Button exitButton;
   
    [Header("Player and Door")]
    public FPController playerMovement;

  public DoorController doorController;



    [Header("Password")]
    public string correctWord = "SPACESHIP";

  private bool puzzleOpen = false;
    private Coroutine feedbackCoroutine;

  void Start()
    {
      // OpenPuzzle();
      if (puzzleCanvas != null)
       puzzleCanvas.SetActive(false);

        if (exitButton != null)
            exitButton.onClick.AddListener(ClosePuzzle);
    }
    void LateUpdate()
    {
       if(puzzleOpen && Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            ClosePuzzle();
        }
       
    }
    void Update()
    {
        if (puzzleOpen && Input.GetKeyDown(KeyCode.Escape))
        {
            ClosePuzzle();
        }
    }
    public void OpenPuzzle()
    {
       if(puzzleCanvas == null || inputField == null)
        {
            Debug.LogWarning("Wordpuzzle: Assign the puzzle Canvas and Input Field.");
            return;
        }
        
        puzzleCanvas.SetActive(true);
        puzzleOpen = true;
        
        if (playerMovement != null)
        playerMovement.SetGameplayInputBlocked(true);
        
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        
        if(feedbackCoroutine != null)
        {
            StopCoroutine(feedbackCoroutine);
            feedbackCoroutine = null;
        }
        
        inputField.interactable = true;
        inputField.text = "";

        if (feedbackText != null)
            feedbackText.text = "";

      
        inputField.ActivateInputField();
        inputField.Select();

    }
    public void ClosePuzzle()
    {
        if (!puzzleOpen)
            return;
        
        if(feedbackCoroutine != null)
        {
            StopCoroutine(feedbackCoroutine);
            feedbackCoroutine = null;
        }
       
        if(puzzleCanvas !=null)
        puzzleCanvas.SetActive(false);
        
        puzzleOpen = false;
       
        if(playerMovement != null)
            playerMovement.SetGameplayInputBlocked(false);
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
    public void CheckAnswer()
    {
        if (!puzzleOpen || inputField == null)
            return;
       
        string answer = inputField.text.Trim();

        if (string.Equals(answer, correctWord.Trim(), System.StringComparison.OrdinalIgnoreCase))
        {
            Debug.Log("Correct answer!");
           
            if (feedbackText != null)
            feedbackText.text = "Door unlocked!";

            if (doorController != null)
            {
                doorController.UnlockDoor();
                doorController.OpenDoor();
            }

            ClosePuzzle();


        }
        else
        {
            Debug.Log("Incorrect");
            
            if(feedbackText != null)
            feedbackText.text = "Incorrect";
            

            if (feedbackCoroutine != null)
            StopCoroutine(feedbackCoroutine);
            
            feedbackCoroutine = StartCoroutine(ClearIncorrectMessage());

        }
    }
        private IEnumerator ClearIncorrectMessage()
    {
        yield return new WaitForSecondsRealtime(2f);

        if (feedbackText != null)
        {
            feedbackText.text = "";
        }

        feedbackCoroutine = null;

        if (inputField != null && puzzleOpen)
        {
            inputField.interactable = true;
            inputField.ActivateInputField();
            inputField.Select();
        }
    }


}

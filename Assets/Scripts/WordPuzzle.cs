using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using System.Collections;

public class WordPuzzle : MonoBehaviour
{
    [Header("Puzzle UI")]
    public GameObject puzzleCanvas;
    public TMP_InputField inputField;
    public TMP_Text feedbackText;
   
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
      if(puzzleCanvas != null)
       puzzleCanvas.SetActive(false);
       
        Cursor.lockState = CursorLockMode.Locked;
       Cursor.visible = false;



    }
    void LateUpdate()
    {
       if(puzzleOpen)
        {
             Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
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
        playerMovement.canMove = false;
        
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

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
       if(feedbackCoroutine != null)
        {
            StopCoroutine(feedbackCoroutine);
            feedbackCoroutine = null;
        }
        if(puzzleCanvas !=null)
        puzzleCanvas.SetActive(false);
        
        puzzleOpen = false;
       
        if(playerMovement != null)
        playerMovement.canMove = true;
        
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
        
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

using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

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

  void Start()
    {
      // OpenPuzzle();
      if((puzzleCanvas != null))
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
        puzzleCanvas.SetActive(true);
        puzzleOpen = true;
        playerMovement.canMove = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        inputField.text = "";
        feedbackText.text = "";

        inputField.ActivateInputField();
        inputField.Select();

    }
    public void ClosePuzzle()
    {
        puzzleCanvas.SetActive(false);
        puzzleOpen = false;
        playerMovement.canMove = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
        
    }
    public void CheckAnswer()
    {
        string answer = inputField.text;

        if (string.Equals(answer, correctWord, System.StringComparison.OrdinalIgnoreCase))
        {
            Debug.Log("Correct answer!");
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
            feedbackText.text = "Incorrect";
            
        }
    }


}

using UnityEngine;

public class PuzzleDoorInteraction : MonoBehaviour , IInteractable
{
    public WordPuzzle wordPuzzle;

    public DoorController doorController;

    public void Interact()
    {
        if (wordPuzzle != null)
        {
            wordPuzzle.OpenPuzzle();
        }
       
    }
    
}

using UnityEngine;

public class ComputerInteractable : MonoBehaviour, IInteractable
{
    public GameObject screenUI;
    public DoorController door;

    private bool interacted = false;
    public void Interact()
    {
        if (interacted)
            return;

        interacted = true;

        Debug.Log("Computer interacted with!");

        // SHOWS THE PLAYER THE SCREEN!
        if (screenUI != null)
        {
            screenUI.SetActive(true);
        }

        // UNLOCKS THE DOOR FOR THE PLAYER!
        if (door != null)
        {
            door.UnlockDoor();
        }
    }
}

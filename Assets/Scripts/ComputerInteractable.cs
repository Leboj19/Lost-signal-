using UnityEngine;

public class ComputerInteractable : MonoBehaviour, IInteractable
{
    [Header("Computer Settings")]
    public GameObject screenUI;
    public DoorController door;

    [Header("Computer Audio")]
    [SerializeField] private AudioSource computerAudio;
    [SerializeField] private AudioClip computerInteractionSound;


    private bool interacted = false;
    public void Interact()
    {
        if (interacted)
            return;

        interacted = true;

        Debug.Log("Computer interacted with!");

        if(computerAudio != null && computerInteractionSound != null)
        {
            computerAudio.PlayOneShot(computerInteractionSound);
        }

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

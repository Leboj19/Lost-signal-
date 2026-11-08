using UnityEngine;
using TMPro;

public class AutomaticDoor : MonoBehaviour
{
    [Header("Door Reference")]
    public DoorController door;

    [Header("Required Signal Ports")]
    public SignalPort[] requiredSignalPorts;

    [Header("Locked Door Message")]
    public GameObject doorMessageUI;
    public TMP_Text doorMessageText;

    [TextArea]
    public string lockedMessage =
       "ACCESS DENIED\nActivate both Signal Ports to unlock this room.";

    private void Start()
    {
        HideDoorMessage();
    }
    private bool playerInside = false;
    private bool blockedMessageShown = false;

   private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;
        
        playerInside = true;
        blockedMessageShown = false;
        
        TryOpenDoor();
        
    }
    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        playerInside = false;
        blockedMessageShown = false;

        HideDoorMessage();

        if (door != null)
        {
            door.CloseDoor();
        }
    }
    private void Update()
    {
        if (playerInside)
        {
            TryOpenDoor();
        }
    }
    private void TryOpenDoor()
    {
        if (door == null)
            return;

        if(requiredSignalPorts == null || requiredSignalPorts.Length == 0)
        {
            HideDoorMessage();
            door.OpenDoor();
            return;
        }
        foreach(SignalPort port in requiredSignalPorts)
        {
            if (port == null || !port.IsActivated)
            {
                ShowDoorMessage();
                return;
            }
         }
        HideDoorMessage();
        door.UnlockDoor();
        door.OpenDoor();
    }
    private void ShowDoorMessage()
    {
        if (doorMessageText != null)
        {
            doorMessageText.text = lockedMessage;
        }

        if (doorMessageUI != null)
        {
            doorMessageUI.SetActive(true);
        }

        if (!blockedMessageShown)
        {
            Debug.Log(lockedMessage);
            blockedMessageShown = true;
        }
    }
    private void HideDoorMessage()
    {
        if (doorMessageUI != null)
        {
            doorMessageUI.SetActive(false);
        }
    }
}

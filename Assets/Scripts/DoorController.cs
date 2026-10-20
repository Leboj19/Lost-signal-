using UnityEngine;

public class DoorController : MonoBehaviour
{
    public Transform door;
    public Vector3 openPosition;
    public float openSpeed = 2f;

    [Header("Door Settings")]
    public bool requiresUnlock = false;

    private Vector3 closedPosition;
    private bool isOpen = false;
    private bool unlocked = false;

     void Start()
    {
        closedPosition = door.localPosition;

        if (!requiresUnlock)
        {
            unlocked = true;
        }
    }
    void Update()
    {
        if (isOpen)
        {
            door.localPosition = Vector3.Lerp(
                door.localPosition,
                openPosition,
                Time.deltaTime * openSpeed
            );
        }
        else
        {
            door.localPosition = Vector3.Lerp(
                door.localPosition,
                closedPosition,
                Time.deltaTime * openSpeed
            );
        }
    }

    public void UnlockDoor()
    {
        unlocked = true;
        Debug.Log("DOOR UNLOCKED!");
    }

    public void OpenDoor()
    {
        if (!unlocked)
        {
            Debug.Log(" Door is locked. Interact with computer first");
            return;
        }
        isOpen = true;
        
    }

    public void CloseDoor()
    {
        isOpen = false;
    }
}



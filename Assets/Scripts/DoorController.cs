using UnityEngine;

public class DoorController : MonoBehaviour
{
    public Transform door;
    public Vector3 openPosition;
    public float openSpeed = 2f;

    private Vector3 closedPosition;
    private bool isOpen = false;

     void Start()
    {
        closedPosition = door.localPosition;
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

    public void OpenDoor()
    {
        isOpen = true;
    }

    public void CloseDoor()
    {
        isOpen = false;
    }
}



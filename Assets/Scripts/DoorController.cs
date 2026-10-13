using UnityEngine;

public class DoorController : MonoBehaviour
{
    [Header("Door Movement")]
    public Transform door;
    public Vector3 openPosition;
    public float openSpeed = 2f;

    [Header("Door Settings")]
    public bool requiresUnlock = false;

    [Header("Door Audio")]
    [SerializeField] private AudioSource doorAudio;
    [SerializeField] private AudioClip doorOpenSound;
    [SerializeField] private AudioClip doorCloseSound;


    private Vector3 closedPosition;
    private bool isOpen = false;
    private bool unlocked = false;

     void Start()
    {
       if(door == null)
        {
            Debug.LogError("Door Transform is not assigned!", this);
            return;
        }
        
        closedPosition = door.localPosition;

        if (!requiresUnlock)
        {
            unlocked = true;
        }
    }
    void Update()
    {
        if (door == null)
            return;
        
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
        if (!isOpen)
        {
            isOpen = true;
            PlayDoorSound(doorOpenSound);
        }
      
        
    }

    public void CloseDoor()
    
    {
        if (isOpen)
        {
            isOpen = false;
            PlayDoorSound(doorCloseSound);
        }
    }
    private void PlayDoorSound(AudioClip clip)
    {
        if(doorAudio != null && clip != null)
        {
            doorAudio.PlayOneShot(clip);
        }
    }
}



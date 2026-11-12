using TMPro;
using UnityEngine;
using System.Collections;

public class DoorController : MonoBehaviour
{
    [Header("Door Movement")]
    public Transform door;
    public Vector3 openPosition;
    public float openSpeed = 2f;

    [Header("Door Settings")]
    public bool requiresUnlock = false;

    [Header("Locked Door Warning")]
    public GameObject warningPanel;
    public TMP_Text warningText;
    [TextArea(2, 4)]
    public string lockedMessage =
        "ACCESS LOCKED\n\nInteract with the computer terminal first.";
    public float warningDuration = 3f;

    [Header("Door Audio")]
    [SerializeField] private AudioSource doorAudio;
    [SerializeField] private AudioClip doorOpenSound;
    [SerializeField] private AudioClip doorCloseSound;


    private Vector3 closedPosition;
    private bool isOpen = false;
    private bool unlocked = false;
    private Coroutine warningCoroutine;


    void Start()
    {
       if(door == null)
        {
            Debug.LogError("Door Transform is not assigned!", this);
            return;
        }
        
        closedPosition = door.localPosition;
        unlocked = !requiresUnlock;

        if (warningPanel != null)
            warningPanel.SetActive(false);

        if (!requiresUnlock)
        {
            unlocked = true;
        }
    }
    void Update()
    {
        if (door == null)
            return;

        Vector3 targetPosition = isOpen ? openPosition : closedPosition;

        door.localPosition = Vector3.Lerp(
            door.localPosition,
            targetPosition,
            Time.deltaTime * openSpeed);

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
            ShowLockedWarning();
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
    private void ShowLockedWarning()
    {
        if (warningPanel == null)
        {
            Debug.LogWarning(
                "Locked door warning panel is not assigned!", this);
            return;
        }
        if (warningText != null)
            warningText.text = lockedMessage;

        warningPanel.SetActive(true);

        if (warningCoroutine != null)
            StopCoroutine(warningCoroutine);

        warningCoroutine = StartCoroutine(HideWarningAfterDelay());
    }
    private IEnumerator HideWarningAfterDelay()
    {
        yield return new WaitForSecondsRealtime(warningDuration);

        if (warningPanel != null)
            warningPanel.SetActive(false);

        warningCoroutine = null;
    }
    private void PlayDoorSound(AudioClip clip)
    {
        if(doorAudio != null && clip != null)
        {
            doorAudio.PlayOneShot(clip);
        }
    }
}



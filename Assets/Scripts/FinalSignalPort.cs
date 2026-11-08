using UnityEngine;

public class FinalSignalPort : MonoBehaviour, IInteractable
{
    [Header("Communication Room")]

    public CommunicationRoomController communicationRoom;

    [Header("Port Audio")]
    // Component inserted: Mixkit, "Interface device click"
    // Source: https://mixkit.co/free-sound-effects/technology/
    public AudioSource portAudio;
    public AudioClip componentInsertedSound;
    
    // Port activated: Mixkit, "Machine activation short"
    // Source: https://mixkit.co/free-sound-effects/high-tech/
    public AudioClip activationSound;


    private bool chipInserted = false;
    private bool batteryInserted = false;

    public void Interact()
    {
        Debug.Log("FINAL SIGNAL PORT INTERACTED!");

        SignalInventory inventory =
            FindFirstObjectByType<SignalInventory>();

        if (inventory == null)
        {
            Debug.Log("Signal Inventory not found!");
            return;
        }
        Debug.Log(
       "Chip: " + inventory.signalChipCount +
       " | Battery: " + inventory.batteryCount
   );
        //SHOWS WHEN PLAYER INSERTS CHIP.
        if (!chipInserted && inventory.HasChip())
        {
            inventory.UseChip();

            chipInserted = true;

            Debug.Log("Signal Chip inserted!");
            PlaySound(componentInsertedSound);

            CheckIfComplete();
            return;
        }
        //SHOWS WHEN PLAYER INSERTS BATTERY.
        if (!batteryInserted && inventory.HasBattery())
        {
            inventory.UseBattery();

            batteryInserted = true;

            Debug.Log("Battery inserted!");
            PlaySound(componentInsertedSound);

            CheckIfComplete();
            return;
        }

        Debug.Log("NO REQUIRED COMPONENT AVAILABLE.");

        //IF PLAYER HAS NOTHING
        if (!chipInserted && !batteryInserted)
        {
            Debug.Log("You need the Signal Chip and Battery!");
        }

        else if (!chipInserted)
        {
            Debug.Log("You still need the Signal Chip!");
        }

        else if (!batteryInserted)
        {
            Debug.Log("You still need the Battery!");
        }
    }
    private void CheckIfComplete()
    {
        if (chipInserted && batteryInserted)
        {
            Debug.Log("BOTH COMPONENTS INSTALLED!");

            if (communicationRoom != null)
            {
                communicationRoom.SystemReady();
            }
            else
            {
                Debug.LogWarning("CommunicationRoomController is not assigned!");
            }

        }


    }
    private void PlaySound(AudioClip clip)
    {
        if(portAudio != null && clip != null)
        {
            portAudio.PlayOneShot(clip);
        }
    }
}

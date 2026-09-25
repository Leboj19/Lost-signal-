using UnityEngine;

public class FinalSignalPort : MonoBehaviour, IInteractable
{
    public CommunicationRoomController communicationRoom;

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

            CheckIfComplete();

            return;
        }
        //SHOWS WHEN PLAYER INSERTS BATTERY.
        if (!batteryInserted && inventory.HasBattery())
        {
            inventory.UseBattery();

            batteryInserted = true;

            Debug.Log("Battery inserted!");

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
        }
    }


}

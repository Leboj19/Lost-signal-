using UnityEngine;

public class SignalInventory : MonoBehaviour
{
    public int signalChipCount = 0;
    public int batteryCount= 0;


    public void AddChip()
    {
        signalChipCount+= 1;

        Debug.Log("Chip added. Total: " + signalChipCount);
    }

    public bool HasChip()
    {
        return signalChipCount > 0;
    }

    public void UseChip()
    {
        if (signalChipCount > 0)
        {
            signalChipCount-= 1;

            Debug.Log("Signal Chip used. Remaining: " + signalChipCount);
        }
    }
    public void AddBattery()
    {
        batteryCount+= 1;

        Debug.Log("Battery added. Total: " + batteryCount);
    }

    public bool HasBattery()
    {
        return batteryCount > 0;
    }

    public void UseBattery()
    {
        if (batteryCount > 0)
        {
            batteryCount-= 1;

            Debug.Log("Battery used. Remaining: "+ batteryCount);
        }
    }
}

using UnityEngine;

public class Battery : MonoBehaviour, IInteractable
{
   public void Interact()
    {
        SignalInventory inventory =
        FindFirstObjectByType<SignalInventory>();

        if (inventory != null)
        {
            inventory.AddBattery();

            Debug.Log("Battery picked up!");

            Destroy(gameObject);
        }
    }
}


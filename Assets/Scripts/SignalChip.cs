using UnityEngine;

public class SignalChip : MonoBehaviour,IInteractable
{
    private bool collected = false;

    public void Interact()
    {
        if (collected)
            return;

        collected = true;
        
        SignalInventory inventory =
            FindFirstObjectByType<SignalInventory>();

        if (inventory != null)
        {
            inventory.AddChip();
            Debug.Log("Signal Chip collected once");

            Destroy(gameObject);
        }
    }
}

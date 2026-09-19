using UnityEngine;

public class ComputerInteractable : MonoBehaviour, IInteractable
{
    public GameObject screenUI;
    public void Interact()
    {
        Debug.Log("Computer interacted with!");
        
        screenUI.SetActive(true);
    }
}

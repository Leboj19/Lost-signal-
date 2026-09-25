using UnityEngine;

public class FinalCommunicationScreen : MonoBehaviour, IInteractable
{
    public CommunicationRoomController communicationRoom;

    public void Interact()
    {
        Debug.Log("Final screen interacted with!");
        if (communicationRoom != null)
        {
            communicationRoom.StartCommunication();
        }
    }
}

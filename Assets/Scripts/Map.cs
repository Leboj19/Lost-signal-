using UnityEngine;
using UnityEngine.InputSystem; // Required for the New Input System

public class MapToggle : MonoBehaviour
{
    [Header("UI Reference")]
    [Tooltip("Drag your Raw Image map GameObject from the Hierarchy here")]
    public GameObject mapUI;
    public GameObject objectivesUI;



    private bool isMapOpen = false;

    private void Start()
    {
        // Ensure the map starts hidden
        if (mapUI != null)
        mapUI.SetActive(false);
        

        if (objectivesUI != null)
       objectivesUI.SetActive(false);
        
    }

    private void Update()
    {
        // Bypasses events! Directly checks if the 'M' key was pressed this frame
        if (Keyboard.current != null && Keyboard.current.mKey.wasPressedThisFrame)
        {
            ToggleMapUI();
        }
    }

    private void ToggleMapUI()
    {
        isMapOpen = !isMapOpen;
        
        if (mapUI != null)
        mapUI.SetActive(isMapOpen);

        if (objectivesUI != null)
            objectivesUI.SetActive(isMapOpen);

    }
}

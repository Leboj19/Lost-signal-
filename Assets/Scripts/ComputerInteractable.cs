using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class ComputerInteractable : MonoBehaviour, IInteractable
{
    public enum ComputerType
    {
        IdentityPuzzle,
        MissionScreen,
        WarningScreen,
        WireRepair
    }

    [Header("Computer Type")]
    public ComputerType computerType = ComputerType.IdentityPuzzle;
    
    [Header("Simple Screen Settings")]
    [TextArea(3, 8)]


    public string displayMessage =
    "MISSION OBJECTIVES\n\n" +
    "Explore the spaceship and restore communications.";

    public float displayDuration = 5f;
    [Header("Computer Settings")]
    public GameObject screenUI;
    public TMP_Text computerMessage;
    public TMP_InputField identityInput;
    public Button verifyButton;


    [Header("Objectives")]
    public GameObject objectivesPanel;


    [Header("Door")]
    public DoorController door;

    [Header("Player")]
    public FPController playerController;



    [Header("Computer Audio")]
    [SerializeField] private AudioSource computerAudio;
    [SerializeField] private AudioClip computerInteractionSound;
    [SerializeField] private AudioClip verificationSound;

    public Button continueButton;

    [Header("Wire Repair")]
    public WireRepairInteractable wireRepair;



    private bool interacted = false;
    private bool computerOpen = false;
    private void Start()
    {
        if (screenUI != null)
            screenUI.SetActive(false);

        if (objectivesPanel != null)
            objectivesPanel.SetActive(false);

        if (playerController == null)
            playerController = FindFirstObjectByType<FPController>();

        if (verifyButton != null)
            verifyButton.onClick.AddListener(VerifyIdentity);

        if (continueButton != null)
            continueButton.onClick.AddListener(CloseObjectives);
    }
    public void Interact()
    {
        if (computerType == ComputerType.WireRepair)
        {
            if (wireRepair == null)
                wireRepair = GetComponent<WireRepairInteractable>();

            if (wireRepair != null)
                wireRepair.Interact();
            else
                Debug.LogWarning("WireRepairInteractable is missing!", this);

            return;
        }
        
        if (computerType == ComputerType.MissionScreen ||
    computerType == ComputerType.WarningScreen)
        {
            if (screenUI != null)
                screenUI.SetActive(true);

            if (computerMessage != null)
                computerMessage.text = displayMessage;

            if (computerAudio != null && computerInteractionSound != null)
                computerAudio.PlayOneShot(computerInteractionSound);

            return;
        }
            
            if (interacted || computerOpen)
            return;

        computerOpen = true;



       

        Debug.Log("Computer interacted with!");

        if (computerAudio != null && computerInteractionSound != null)
        {
            computerAudio.PlayOneShot(computerInteractionSound);
        }

        // SHOWS THE PLAYER THE SCREEN!
        if (screenUI != null)
        {
            screenUI.SetActive(true);
        }

        if (computerMessage != null)
        {
            computerMessage.text =
                "SYSTEM LOCKED\n\n" +
                "What is the identity of the robot?";
        }
        if (identityInput != null)
        {
            identityInput.gameObject.SetActive(true);
            identityInput.text = "";
            identityInput.interactable = true;
        }
        if (playerController != null)
            playerController.SetGameplayInputBlocked(true);

        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        if (identityInput != null)
            identityInput.ActivateInputField();



      
    }
    public void VerifyIdentity()
    {
        Debug.Log("VERIFY BUTTON PRESSED!");
        
        if (!computerOpen || interacted)
            return;

        string answer = "";

        if (identityInput != null)
            answer = identityInput.text.Trim();

        Debug.Log("Identity entered: [" + answer + "]");
       
        if (answer.ToUpper() != "RS-Q2")
        {
            if (computerMessage != null)
            {
                computerMessage.text =
                    "IDENTITY NOT RECOGNISED\n\n" +
                    "Please try again.";
            }
            if (identityInput != null)
            {
                identityInput.interactable = true;
                identityInput.gameObject.SetActive(true);
                identityInput.ActivateInputField();
                identityInput.Select();
            }
                

            return;
        }
        interacted = true;

        StartCoroutine(CompleteVerification());
    }
    private IEnumerator CompleteVerification()
    {
        if (computerMessage != null)
        {
            computerMessage.text =
                "VERIFICATION COMPLETE\n\n" +
                "IDENTITY: RSQ2\n" +
                "ACCESS GRANTED.";
        }

        if (computerAudio != null && verificationSound)

        {
            computerAudio.PlayOneShot(verificationSound);
        }

        if (identityInput != null)
            identityInput.gameObject.SetActive(false);

        if (verifyButton != null)
            verifyButton.gameObject.SetActive(false);

        yield return new WaitForSecondsRealtime(2f);

        if (screenUI != null)
            screenUI.SetActive(false);

        if (objectivesPanel != null)
            objectivesPanel.SetActive(true);

        yield return new WaitForSecondsRealtime(1.5f);

        // UNLOCKS THE DOOR FOR THE PLAYER!
        if (door != null)
        {
            door.UnlockDoor();
            door.OpenDoor();
        }
        computerOpen = false;
       
        if (playerController != null)
            playerController.SetGameplayInputBlocked(true);
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

    }
    public void CloseObjectives()
    {
        if (objectivesPanel != null)
            objectivesPanel.SetActive(false);

        if (playerController != null)
            playerController.SetGameplayInputBlocked(false);
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}

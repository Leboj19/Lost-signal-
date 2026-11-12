using UnityEngine;
using TMPro;
using System.Collections;

public class FriendlyNPCInteractable : MonoBehaviour, IInteractable
{
    [Header("NPC Dialogue")]
    public GameObject dialoguePanel;
    public TMP_Text npcNameText;
    public TMP_Text dialogueText;

    [Header("Player Controller")]
    public FPController playerController;

    [Header("Player Inventory")]
    public SignalInventory signalInventory;

    [Header("Signal Port References")]
    public SignalPort signalPort1;
    public SignalPort signalPort2;

    [Header("NPC Movement")]
    public float turnSpeed = 3f;

    [Header("Dialogue Messages")]

    [TextArea(3, 5)]
    public string startingDialogue =
    "Crew member, our communication systems have been severely damaged. " +
    "You must locate TWO Signal Chips and use them to activate both Signal Ports. " +
    "This will restore the connection and unlock the final Communication Room.";

    [TextArea(3, 5)]
    public string oneChipDialogue =
        "Good work, crew member. You have recovered ONE Signal Chip. " +
        "There is still one more Signal Chip somewhere aboard the ship. " +
        "Find it so you can activate the second Signal Port.";

    [TextArea(3, 5)]
    public string portsDialogue =
        "Excellent work! Both Signal Ports have been activated. " +
        "The final Communication Door should now be accessible. " +
        "Proceed to the Communication Room, but remember that the final system requires ONE additional Signal Chip and a Battery.";

    [TextArea(3, 5)]
    public string finalChipDialogue =
        "The final Communication Room is now accessible. " +
        "You still need ONE Signal Chip and a Battery to power the communication system. " +
        "Search the remaining ship compartments.";

    [TextArea(3, 5)]
    public string finalItemsDialogue =
        "Outstanding, crew member! You have recovered the final Signal Chip and Battery. " +
        "Take them to the Communication Screen and restore our connection with the main ship.";

    [TextArea(3, 5)]
    public string readyForPortsDialogue =
    "Excellent! You have recovered both Signal Chips. " +
    "Take one to each Signal Port to restore the ship's communication network.";

    [TextArea(3, 5)]
    public string finalBatteryDialogue =
        "Good work! You have recovered the final Signal Chip. " +
        "The only thing missing is a Battery. Find one and return to the Communication Screen.";

    [TextArea(3, 5)]
    public string finalChipOnlyDialogue =
        "You have recovered the Battery. " +
        "Now find ONE final Signal Chip to power the Communication Screen.";

    public void Interact()
    {
        if (dialoguePanel == null || playerController == null)
            return;

        if (npcNameText != null)
            npcNameText.text = "00-B2 | Maintenance Assistant";

        UpdateDialogue();

        dialoguePanel.SetActive(true);

        StartCoroutine(TurnTowardsPlayer());

        playerController.SetGameplayInputBlocked(true);
    }

    private void UpdateDialogue()
    {
        if (dialogueText == null)
            return;

        if (signalInventory == null ||
            signalPort1 == null ||
            signalPort2 == null)
        {
            dialogueText.text = startingDialogue;
            return;
        }

        int chips = signalInventory.signalChipCount;
        int batteries = signalInventory.batteryCount;

        int activatedPorts = 0;

        if (signalPort1.IsActivated)
            activatedPorts++;

        if (signalPort2.IsActivated)
            activatedPorts++;

        // IF BOTH SIGNAL PORTS ACTIVATED DO THIS.... 
        if (activatedPorts == 2)
        {
            if (chips > 0 && batteries > 0)
            {
                dialogueText.text = finalItemsDialogue;
            }
            else if (chips > 0)
            {
                dialogueText.text = finalBatteryDialogue;
            }
            else if (batteries > 0)
            {
                dialogueText.text = finalChipOnlyDialogue;
            }
            else
            {
                dialogueText.text = portsDialogue;
            }
        }

        // IF ONLY ONE SIGNAL PORT ACTIVATED DO THIS... 
        else if (activatedPorts == 1)
        {
            if (chips > 0)
            {
                dialogueText.text =
                    "Good work, crew member. One Signal Port is active. " +
                    "You have another Signal Chip. Take it to the remaining Signal Port.";
            }
            else
            {
                dialogueText.text =
                    "Good work, crew member. One Signal Port is active. " +
                    "Find the remaining Signal Chip and activate the second port.";
            }
        }

        // IF NO SIGNAL PORTS ACTIVATED DO THIS....
        else
        {
            if (chips >= 2)
            {
                dialogueText.text = readyForPortsDialogue;
            }
            else if (chips == 1)
            {
                dialogueText.text = oneChipDialogue;
            }
            else
            {
                dialogueText.text = startingDialogue;
            }
        }
    }

    public void CloseDialogue()
    {
        if (dialoguePanel == null || playerController == null)
            return;

        dialoguePanel.SetActive(false);

        playerController.SetGameplayInputBlocked(false);
    }

    private IEnumerator TurnTowardsPlayer()
    {
        Vector3 direction = playerController.transform.position - transform.position;

        direction.y = 0f;

        if (direction == Vector3.zero)
            yield break;

        Quaternion targetRotation = Quaternion.LookRotation(direction);

        while (Quaternion.Angle(transform.rotation, targetRotation) > 1f)
        {
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                turnSpeed * Time.deltaTime
            );

            yield return null;
        }

        transform.rotation = targetRotation;
    }
}


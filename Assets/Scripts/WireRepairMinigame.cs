using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class WireRepairMinigame : MonoBehaviour
{
    [Header("Repair UI")]
    public GameObject repairPanel;
    public Button[] leftTerminals;
    public Button[] rightTerminals;
    public Image[] wireLines;
    public TextMeshProUGUI progressText;
    public TextMeshProUGUI messageText;

    [Header("Wire Matching")]
    //0 = Green, 1= Black , 2 = Red
    //Left buttons:Green,Black,Red 
    public int[] leftWireIds = { 0, 1, 2 };

    //Right buttons:Green,Black,Red 
    public int[] rightWireIds = { 2, 0, 1};

    [Header("Player")]
    public FPController playerController;

    private bool[] connected = new bool[3];
    private int selectedLeft = -1;
    private bool isOpen = false;
    private WireRepairInteractable currentScreen;
    void Awake()
    {
        for(int i = 0; i< leftTerminals.Length; i++)
        {
            int index = i;
            leftTerminals[i].onClick.AddListener(() => SelectLeft(index));
        }
        for (int i = 0; i < rightTerminals.Length; i++)
        {
            int index = i;
            rightTerminals[i].onClick.AddListener(() => SelectRight(index));
        }
        wireLines[0].color = Color.green;
        wireLines[1].color = Color.black;
        wireLines[2].color = Color.red;

        repairPanel.SetActive(false);
    }
    public void Open( WireRepairInteractable screen)
    {
        if (isOpen)
            return;

        currentScreen = screen;
        selectedLeft = -1;
        connected= new bool[3];
        isOpen = true;

        foreach (Image wire in wireLines)
            wire.enabled = false;

        foreach (Button button in leftTerminals)
            button.interactable = true;

        foreach (Button button in rightTerminals)
            button.interactable = true;

        messageText.text = "Select a wire on the left.";
        UpdateProgress();

        repairPanel.SetActive(true);

        if (playerController != null)
            playerController.SetGameplayInputBlocked(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    
}
 void SelectLeft(int index)
    {
        if (connected[leftWireIds[index]])
        {
            messageText.text = "This wire is already conncted.";
            return;
        }
        selectedLeft = index;
        messageText.text = "Now Select the matching colour.";
    }
    void SelectRight(int index)
    {
        if(selectedLeft == -1)
        {
            messageText.text = "Select a wire on the left first.";
            return;
        }
        int leftId = leftWireIds[selectedLeft];
        int rightId = rightWireIds[index];

        if(leftId == rightId)
        {
            connected[leftId] = true;

            leftTerminals[selectedLeft].interactable = false;
            rightTerminals[index].interactable = false;

            DrawWire(selectedLeft, index, leftId);

            selectedLeft = -1;
            UpdateProgress();

            if (AllConnected())
            {
                messageText.text = "POWER RESTORED!";
                Invoke(nameof(FinishRepair), 1f);
            }
            else
            {
                messageText.text = "Correct connection!";
            }
        }
        else
        {
            selectedLeft = -1;
            messageText.text = "Wrong connection. Try again.";
        }
    }
    void DrawWire(int leftIndex, int rightIndex, int wireId)
    {
        Image wire = wireLines[wireId];

        RectTransform line = wire.rectTransform;
        RectTransform parent = line.parent as RectTransform;

        Canvas canvas = repairPanel.GetComponentInParent<Canvas>();
        Camera cam = canvas.renderMode == RenderMode.ScreenSpaceOverlay
            ? null
            : canvas.worldCamera;

        Vector2 start;
        Vector2 end;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            parent,
            RectTransformUtility.WorldToScreenPoint(
                cam, leftTerminals[leftIndex].transform.position),
            cam,
            out start);

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            parent,
            RectTransformUtility.WorldToScreenPoint(
                cam, rightTerminals[rightIndex].transform.position),
            cam,
            out end);

        Vector2 direction = end - start;

        line.anchoredPosition = (start + end) / 2f;
        line.sizeDelta = new Vector2(direction.magnitude, 8f);

        line.localRotation = Quaternion.Euler(
            0f, 0f,
            Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);

        wire.enabled = true;
    }
    void UpdateProgress()
    {
        int count = 0;

        foreach (bool value in connected)
        {
            if (value)
                count++;
        }

        progressText.text = "WIRES CONNECTED: " + count + "/3";
    }
    bool AllConnected()
    {
        foreach (bool value in connected)
        {
            if (!value)
                return false;
        }

        return true;
    }
    void FinishRepair()
    {
        if (!isOpen)
            return;

        if (currentScreen != null)
            currentScreen.RepairCompleted();

        Close();
    }
    public void Close()
    {
        if (!isOpen)
            return;

        CancelInvoke(nameof(FinishRepair));

        isOpen = false;
        repairPanel.SetActive(false);

        if (playerController != null)
            playerController.SetGameplayInputBlocked(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        selectedLeft = -1;
        currentScreen = null;
    }
}
    


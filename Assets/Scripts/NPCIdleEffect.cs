using UnityEngine;

public class NPCIdleEffect : MonoBehaviour
{
    [Header("Sensor Material")]
    public Renderer sensorRenderer;

    [Header("Mission References")]
    public SignalInventory signalInventory;
    public SignalPort signalPort1;
    public SignalPort signalPort2;

    [Header("Sensor Colours")]
    public Color idleColor = Color.cyan;
    public Color collectingColor = Color.yellow;
    public Color portsActivatedColor = new Color(0.65f, 0.2f, 1f);
    public Color missionReadyColor = Color.green;

    [Header("Pulse Settings")]
    public float minimumIntensity = 0.2f;
    public float maximumIntensity = 0.8f;
    public float pulseSpeed = 1.5f;

    [Header("Colour Transition")]
    public float colourChangeSpeed = 3f;

    private Material sensorMaterial;
    private Color currentColor;

    private void Start()
    {
        if (sensorRenderer != null)
        {
            sensorMaterial = sensorRenderer.material;
        }

        currentColor = idleColor;
    }

    private void Update()
    {
        if (sensorMaterial == null)
            return;

        Color targetColor = GetMissionColor();

        currentColor = Color.Lerp(
            currentColor,
            targetColor,
            colourChangeSpeed * Time.deltaTime
        );

        float pulse = (Mathf.Sin(Time.time * pulseSpeed) + 1f) / 2f;

        float intensity = Mathf.Lerp(
            minimumIntensity,
            maximumIntensity,
            pulse
        );

        sensorMaterial.EnableKeyword("_EMISSION");

        sensorMaterial.SetColor(
            "_EmissionColor",
            currentColor * intensity
        );
    }

    private Color GetMissionColor()
    {
        if (signalInventory == null ||
            signalPort1 == null ||
            signalPort2 == null)
        {
            return idleColor;
        }

        int chips = signalInventory.signalChipCount;
        int batteries = signalInventory.batteryCount;

        bool bothPortsActive =
            signalPort1.IsActivated &&
            signalPort2.IsActivated;

        if (bothPortsActive && chips > 0 && batteries > 0)
        {
            return missionReadyColor;
        }

        if (bothPortsActive)
        {
            return portsActivatedColor;
        }

        if (chips > 0 ||
            signalPort1.IsActivated ||
            signalPort2.IsActivated)
        {
            return collectingColor;
        }

        return idleColor;
    }
}
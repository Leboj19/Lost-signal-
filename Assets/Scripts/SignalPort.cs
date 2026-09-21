using System.Collections;
using UnityEngine;

public class SignalPort : MonoBehaviour,IInteractable
{
    public Renderer leftGlow;
    public Renderer rightGlow;

    public Material offMaterial;
    public Material redMaterial;
    public Material purpleMaterial;

    public float flickerDuration = 3f;

    private bool activated = false;
    private bool flickering = false;

    private void Start()
    {
        SetOff();
    }

    public void Interact()
    {
        if (activated)
        {
            Debug.Log("Signal Port already activated.");
            return;
        }


        if (flickering)
        {
            return;
        }

        SignalInventory inventory =
            FindFirstObjectByType<SignalInventory>();

        if (inventory != null && inventory.HasChip())
        {
            inventory.UseChip();

            StartCoroutine(FlickerThenActivate());
        }
        else
        {
            Debug.Log("You need the Signal Chip!");
        }

    }

    private IEnumerator FlickerThenActivate()
    {
        flickering = true;

        float timer = 0f;

        while (timer < flickerDuration)
        {
            SetRed();

            yield return new WaitForSeconds(0.15f);

            SetOff();

            yield return new WaitForSeconds(0.1f);

            timer += 0.25f;
        }

        SetPurple();

        activated = true;
        flickering = false;

        Debug.Log("SIGNAL RESTORED!");
    }

    private void SetOff()
    {
        leftGlow.material = offMaterial;
        rightGlow.material = offMaterial;
    }

    private void SetRed()
    {
        leftGlow.material = redMaterial;
        rightGlow.material = redMaterial;
    }

    private void SetPurple()
    {
        leftGlow.material = purpleMaterial;
        rightGlow.material = purpleMaterial;
    }
}

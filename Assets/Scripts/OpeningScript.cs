using UnityEngine;
using TMPro;

public class OpeningScript : MonoBehaviour
{
    public TextMeshProUGUI titleText;
    public float phaseInDuration = 2.5f;

    private float timer = 0f;

    void Start()
    {
        SetAlpha(0f);
    }

    void Update()
    {
        if (timer < phaseInDuration)
        {
            timer += Time.deltaTime;
            float progress = timer / phaseInDuration;

            // Flicker chance decreases as progress increases
            float flickerChance = 1f - progress;
            float alpha = Random.value < flickerChance
                ? Random.Range(0f, 1f)
                : progress;

            SetAlpha(alpha);
        }
        else
        {
            SetAlpha(1f); // lock fully visible once done
        }
    }

    void SetAlpha(float alpha)
    {
        Color c = titleText.color;
        c.a = alpha;
        titleText.color = c;
    }
}
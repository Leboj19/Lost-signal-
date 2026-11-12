using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;
using UnityEngine.SceneManagement;

public class StaticScript : MonoBehaviour
{
    public int noiseWidth = 256;
    public int noiseHeight = 144;
    public float fadeInTime = 0.2f;
    public float holdTime = 1.5f;
    public float fadeOutTime = 1.5f;
    public UnityEvent onFinished;
    public string nextScene = "Instructions";

    private CanvasGroup group;
    private Texture2D noiseTex;
    private Color32[] pixels;
    private bool running = false;

    void Awake()
    {

        var canvasGO = new GameObject("StaticOverlay", typeof(RectTransform));
        var canvas = canvasGO.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.sortingOrder = 999;

        group = canvasGO.AddComponent<CanvasGroup>();
        group.alpha = 0f;
        group.blocksRaycasts = false;
        group.interactable = false;

        var imgGO = new GameObject("StaticImage", typeof(RectTransform));
        imgGO.transform.SetParent(canvasGO.transform, false);
        var raw = imgGO.AddComponent<RawImage>();
        raw.rectTransform.anchorMin = Vector2.zero;
        raw.rectTransform.anchorMax = Vector2.one;
        raw.rectTransform.offsetMin = Vector2.zero;
        raw.rectTransform.offsetMax = Vector2.zero;

        noiseTex = new Texture2D(noiseWidth, noiseHeight, TextureFormat.RGBA32, false);
        noiseTex.filterMode = FilterMode.Point;
        pixels = new Color32[noiseWidth * noiseHeight];
        raw.texture = noiseTex;
    }

    [ContextMenu("Test Show")]
    public void Show()
    {
        Debug.Log("StaticScript.Show() called");
        if (!running) StartCoroutine(StaticRoutine());
    }

    IEnumerator StaticRoutine()
    {
      running = true;
    yield return Fade(0f, 1f, fadeInTime);
    yield return new WaitForSeconds(holdTime);
    yield return Fade(1f, 0f, fadeOutTime);
    onFinished?.Invoke();

    if (!string.IsNullOrEmpty(nextScene))
        SceneManager.LoadScene(nextScene);

    running = false;
    }

    IEnumerator Fade(float from, float to, float duration)
    {
        float t = 0f;
        while (t < duration)
        {
            t += Time.deltaTime;
            group.alpha = Mathf.Lerp(from, to, t / duration);
            yield return null;
        }
        group.alpha = to;
    }

    void Update()
    {
        if (group == null || group.alpha <= 0f) return;

        for (int i = 0; i < pixels.Length; i++)
        {
            byte v = (byte)Random.Range(0, 256);
            pixels[i] = new Color32(v, v, v, 255);
        }
        noiseTex.SetPixels32(pixels);
        noiseTex.Apply();
    }
}
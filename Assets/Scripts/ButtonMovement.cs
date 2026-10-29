using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonMovement: MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Movement")]
    public Vector2 hoverOffset = new Vector2(8f, 0f); // uhhh remember this for pixel speed
    public float speed = 12f; 
    private RectTransform rect;
    private Vector2 originalPos;
    private Vector2 targetPos;

    void Awake()
    {
        rect = GetComponent<RectTransform>();
        originalPos = rect.anchoredPosition;
        targetPos = originalPos;
    }

    void Update()
    {
        rect.anchoredPosition = Vector2.Lerp(rect.anchoredPosition, targetPos, Time.unscaledDeltaTime * speed);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        targetPos = originalPos + hoverOffset;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        targetPos = originalPos;
    }
}

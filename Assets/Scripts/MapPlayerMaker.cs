using UnityEngine;

public class MapPlayerMaker : MonoBehaviour
{
    [Header("References")]
    public Transform player;
    public RectTransform mapArea;
    public RectTransform playerDot;

    [Header("Ship World Bounds")]
    public Vector2 worldMin = new Vector2(-20f, -20f);
    public Vector2 worldMax = new Vector2(20f, 20f);

   
    
    void LateUpdate()
    {
        if (player == null || mapArea == null || playerDot == null)
            return;

       
        float width = worldMax.x - worldMin.x;
        float height = worldMax.y - worldMin.y;

        if (width <= 0f || height <= 0f)
            return;

        float mapX = Mathf.InverseLerp(
            worldMin.x, worldMax.x, player.position.x);

        float mapY = Mathf.InverseLerp(
            worldMin.y, worldMax.y, player.position.z);

       
        mapX = Mathf.Clamp01(mapX);
        mapY = Mathf.Clamp01(mapY);

        playerDot.anchoredPosition = new Vector2(
            (mapX - 0.5f) * mapArea.rect.width,
            (mapY - 0.5f) * mapArea.rect.height
        );
    }
}

using UnityEngine;

public class TwoColorBackground : MonoBehaviour
{
    [Header("Top Half (Sky)")]
    public Color topColor = new Color(0.4f, 0.7f, 1f); // Bright Sky Blue

    [Header("Bottom Half (Ground)")]
    public Color bottomColor = new Color(0.1f, 0.05f, 0.05f); // Dark Brown/Black

    [Header("Size")]
    public float width = 50f;   // How wide your level is
    public float height = 15f;  // How tall the background is
    public float splitHeight = 0.5f; // 0.5 = exactly half, 0.3 = 30% sky, 70% ground

    void Start()
    {
        // Create a 2-pixel wide, 2-pixel tall texture (just enough for 2 colors)
        int texWidth = 2;
        int texHeight = 2;
        Texture2D tex = new Texture2D(texWidth, texHeight);
        tex.wrapMode = TextureWrapMode.Clamp;

        // Fill it with 2 colors: Top = sky, Bottom = ground
        for (int y = 0; y < texHeight; y++)
        {
            // If y is 1 (top), use topColor. If y is 0 (bottom), use bottomColor.
            Color color = (y == 1) ? topColor : bottomColor;
            for (int x = 0; x < texWidth; x++)
            {
                tex.SetPixel(x, y, color);
            }
        }
        tex.Apply();

        // Convert to a Sprite
        Sprite sprite = Sprite.Create(tex, new Rect(0, 0, texWidth, texHeight), new Vector2(0.5f, 0.5f));

        // Assign it to this GameObject
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr == null) sr = gameObject.AddComponent<SpriteRenderer>();
        
        sr.sprite = sprite;
        sr.drawMode = SpriteDrawMode.Sliced;

        // Stretch it to your level size
        transform.localScale = new Vector3(width, height, 1);

        // Push it to the very back
        sr.sortingOrder = -10;

        // Adjust the split point (where the colors meet)
        // We shift the sprite up/down to control the split
        // 0 = bottom of sprite, 1 = top of sprite
        float splitOffset = splitHeight - 0.5f;
        transform.position = new Vector3(transform.position.x, transform.position.y + (splitOffset * height), transform.position.z);
    }
}
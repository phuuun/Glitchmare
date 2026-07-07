using UnityEngine;

public class PatrolEnemy : MonoBehaviour
{
    // Choose if it moves left/right OR up/down
    public enum MoveAxis { Horizontal, Vertical }
    
    [Header("Movement Settings")]
    public MoveAxis axis = MoveAxis.Horizontal; // Default is left/right
    public float speed = 2f;                   // How fast it moves
    public float distance = 3f;                // How far from its start point it goes

    private Vector2 startPosition;
    private SpriteRenderer spriteRenderer;
    private float previousOffset = 0f;         // Used to figure out which way it's moving

    void Start()
    {
        // Remember where the enemy was placed in the scene
        startPosition = transform.position;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // PingPong makes a number go from 0 -> distance -> 0 -> distance -> forever
        float offset = Mathf.PingPong(Time.time * speed, distance);
        
        // Decide if we move left/right or up/down
        Vector2 direction = (axis == MoveAxis.Horizontal) ? Vector2.right : Vector2.up;
        
        // Apply the movement
        transform.position = startPosition + (direction * offset);

        // --- MIRROR THE SPRITE (Only for Horizontal) ---
        if (axis == MoveAxis.Horizontal && spriteRenderer != null)
        {
            // If the offset is increasing, we are moving RIGHT
            if (offset > previousOffset + 0.001f)
            {
                spriteRenderer.flipX = false; // Face RIGHT (default)
            }
            // If the offset is decreasing, we are moving LEFT
            else if (offset < previousOffset - 0.001f)
            {
                spriteRenderer.flipX = true;  // Face LEFT (mirrored)
            }
            previousOffset = offset;
        }
    }
}
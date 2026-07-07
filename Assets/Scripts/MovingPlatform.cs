using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    // Choose if it moves left/right OR up/down
    public enum MoveAxis { Horizontal, Vertical }
    
    [Header("Movement Settings")]
    public MoveAxis axis = MoveAxis.Horizontal;
    public float speed = 2f;
    public float distance = 3f;

    private Vector2 startPosition;
    private Rigidbody2D rb;

    void Start()
    {
        // Remember where we started
        startPosition = transform.position;
        
        // Get or add a Rigidbody2D (MUST be Kinematic)
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody2D>();
        }
        
        // Kinematic means "I control the movement, physics doesn't push me"
        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    void FixedUpdate() // Physics update!
    {
        // PingPong goes 0 -> distance -> 0 -> distance
        float offset = Mathf.PingPong(Time.time * speed, distance);
        
        // Decide direction
        Vector2 direction = (axis == MoveAxis.Horizontal) ? Vector2.right : Vector2.up;
        
        // Calculate where we want to be
        Vector2 targetPosition = startPosition + (direction * offset);
        
        // THIS IS THE MAGIC: MovePosition pushes the player with the platform!
        rb.MovePosition(targetPosition);
    }
}
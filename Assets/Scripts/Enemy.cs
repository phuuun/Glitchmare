using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    public enum MoveAxis { Horizontal, Vertical }
    
    [Header("Movement")]
    public MoveAxis axis = MoveAxis.Horizontal;
    public float speed = 2f;
    public float distance = 3f;
    
    [Header("Squash Stomp")]
    public Sprite squashSprite;  // <-- Drag your squashed sprite here!
    public float bounceForce = 12f; // How high the player bounces

    private Vector2 startPosition;
    private SpriteRenderer spriteRenderer;
    private Collider2D enemyCollider;
    private bool isSquashed = false;

    void Start()
    {
        startPosition = transform.position;
        spriteRenderer = GetComponent<SpriteRenderer>();
        enemyCollider = GetComponent<Collider2D>();
    }

    void Update()
    {
        if (isSquashed) return; // Stop moving when dead!

        float offset = Mathf.PingPong(Time.time * speed, distance);
        Vector2 direction = (axis == MoveAxis.Horizontal) ? Vector2.right : Vector2.up;
        transform.position = startPosition + (direction * offset);

        // Flipping the sprite to face the direction it moves (Horizontal only)
        if (axis == MoveAxis.Horizontal && spriteRenderer != null)
        {
            float dir = Mathf.Sin(Time.time * speed);
            if (dir > 0.1f) spriteRenderer.flipX = false; // Face Right
            else if (dir < -0.1f) spriteRenderer.flipX = true; // Face Left
        }
    }

    // This detects when the player touches the enemy
    void OnCollisionEnter2D(Collision2D collision)
{
    if (isSquashed) return;
    if (!collision.gameObject.CompareTag("Player")) return;

    ContactPoint2D contact = collision.GetContact(0);

    if (contact.normal.y < -0.5f)
    {
        Stomp(collision.gameObject);
    }
    else
    {
        KillPlayer(collision.gameObject);
    }
}

void KillPlayer(GameObject playerObject)
{
    PlayerHealth health = playerObject.GetComponent<PlayerHealth>();

    if (health != null)
    {
        health.TakeDamage();
    }
}

    void Stomp(GameObject player)
    {
        isSquashed = true;
        AudioManager.Instance.PlaySFX(AudioManager.Instance.stomp);
        // 1. Change sprite to the squashed one!
        if (squashSprite != null)
        {
            spriteRenderer.sprite = squashSprite;
        }
        else
        {
            spriteRenderer.enabled = false; // Hide if you don't have a squash sprite
        }

        // 2. Disable collider so player doesn't bump into it again
        if (enemyCollider != null)
            enemyCollider.enabled = false;

        // 3. BOUNCE THE PLAYER UP! (like Mario)
        Rigidbody2D playerRb = player.GetComponent<Rigidbody2D>();
        if (playerRb != null)
        {
            playerRb.linearVelocity = new Vector2(playerRb.linearVelocity.x, bounceForce);
        }

        // 4. Destroy the enemy after 0.3 seconds (so you can see the squash)
        Destroy(gameObject, 0.3f);
    }
}
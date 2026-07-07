using UnityEngine;

public class MarioBackground : MonoBehaviour
{
    [Header("Settings")]
    public Transform target;        // Drag your Camera or Player here
    public float parallaxSpeed = 0.2f; // 0 = static, 0.2 = slow, 1 = moves with player

    private Vector3 lastTargetPosition;
    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
        lastTargetPosition = target.position;
    }

    void LateUpdate()
    {
        if (target == null) return;

        // Calculate how much the target moved this frame
        float deltaX = target.position.x - lastTargetPosition.x;

        // Move the background by a fraction of that distance
        transform.position += new Vector3(deltaX * parallaxSpeed, 0, 0);

        // Store the target's position for the next frame
        lastTargetPosition = target.position;
    }
}
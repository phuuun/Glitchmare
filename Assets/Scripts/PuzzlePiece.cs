using UnityEngine;

public class PuzzlePiece : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;
        AudioManager.Instance.PlaySFX(AudioManager.Instance.puzzle);
        PuzzleManager.Instance.CollectPiece();

        Destroy(gameObject);
    }
}
using UnityEngine;

public class Portal : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        WinScreen ws = FindAnyObjectByType<WinScreen>(FindObjectsInactive.Include);

        if (ws != null)
        {
            ws.Show();
        }
    }
}
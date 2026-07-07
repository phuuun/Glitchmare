using UnityEngine;
using TMPro;

public class PuzzleManager : MonoBehaviour
{
    public static PuzzleManager Instance;

    public TMP_Text puzzleText;

    public int collectedPieces = 0;
    public int totalPieces = 9;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        UpdateUI();
    }

    public void CollectPiece()
    {
        collectedPieces++;

        if (collectedPieces > totalPieces)
            collectedPieces = totalPieces;

        UpdateUI();
    }

    void UpdateUI()
    {
        puzzleText.text =
            "Puzzle Pieces\n" +
            collectedPieces + " / " + totalPieces;
    }

    public bool HasAllPieces()
    {
        return collectedPieces >= totalPieces;
    }
}
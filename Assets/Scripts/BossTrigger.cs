using System.Collections;
using UnityEngine;
using TMPro;

public class BossTrigger : MonoBehaviour
{
    [Header("UI")]
    public TMP_Text promptText;
    [Header("Boss Settings")]
    public bool isLevel2Boss = false;

    [Header("Objects")]
    public GameObject spellBubble;
    public GameObject fire;
    public GameObject barrier;
    public Animator bossAnimator;

    private PlayerController player;
    private bool playerInside = false;
    private bool bossDefeated = false;

    void Update()
    {
        if (!playerInside || bossDefeated)
            return;

        if (PuzzleManager.Instance.HasAllPieces())
        {
            promptText.text = "Press ENTER to Cast Spell";

            if (Input.GetKeyDown(KeyCode.Return))
            {
                StartCoroutine(CastSpell());
            }
        }
        else
        {
            promptText.text =
                "Need All Puzzle Pieces\n" +
                PuzzleManager.Instance.collectedPieces +
                "/9";
        }
    }

    IEnumerator CastSpell()
    {
        bossDefeated = true;

        promptText.text = "";

        player.enabled = false;

        spellBubble.SetActive(true);

        yield return new WaitForSeconds(2f);

        spellBubble.SetActive(false);

        fire.SetActive(true);
        AudioManager.Instance.PlaySFX(AudioManager.Instance.fire);
        yield return new WaitForSeconds(0.5f);

        bossAnimator.SetTrigger("Die");

        if (isLevel2Boss)
        {
            AudioManager.Instance.PlaySFX(AudioManager.Instance.bossDie2);
        }
        else
        {
            AudioManager.Instance.PlaySFX(AudioManager.Instance.bossDie1);
        }

        yield return new WaitForSeconds(1f);

        Destroy(barrier);

        player.enabled = true;

        yield return new WaitForSeconds(2f);

        fire.SetActive(false);

        Destroy(bossAnimator.gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        playerInside = true;
        player = other.GetComponent<PlayerController>();
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        playerInside = false;

        promptText.text = "";
    }
}
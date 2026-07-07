using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health")]
    public int maxHealth = 3;
    private int currentHealth;

    [Header("Heart UI")]
    public Image heart1;
    public Image heart2;
    public Image heart3;

    public Sprite fullHeart;
    public Sprite emptyHeart;

    [Header("Damage")]
    public float invincibleTime = 1f;
    private bool isInvincible = false;

    private SpriteRenderer spriteRenderer;
    private PlayerController playerController;

    void Start()
    {
        currentHealth = maxHealth;

        spriteRenderer = GetComponent<SpriteRenderer>();
        playerController = GetComponent<PlayerController>();

        UpdateHearts();
    }

    public void TakeDamage()
    {
        if (isInvincible)
            return;

        currentHealth--;

        UpdateHearts();

        if (currentHealth <= 0)
        {
            playerController.Die();
            return;
        }

        StartCoroutine(DamageRoutine());
    }

    IEnumerator DamageRoutine()
    {
        isInvincible = true;

        // Flash red
        for (int i = 0; i < 5; i++)
        {
            spriteRenderer.color = Color.red;
            yield return new WaitForSeconds(0.1f);

            spriteRenderer.color = Color.white;
            yield return new WaitForSeconds(0.1f);
        }

        isInvincible = false;
    }

    void UpdateHearts()
    {
        heart1.sprite = currentHealth >= 1 ? fullHeart : emptyHeart;
        heart2.sprite = currentHealth >= 2 ? fullHeart : emptyHeart;
        heart3.sprite = currentHealth >= 3 ? fullHeart : emptyHeart;
    }
}
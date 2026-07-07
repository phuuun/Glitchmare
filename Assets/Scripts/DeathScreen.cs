using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreen : MonoBehaviour
{
    [Header("Images")]
    public GameObject retryHighlighted;
    public GameObject quitHighlighted;

    private bool isRetrySelected = true;
    private bool isActive = false;

    void Update()
    {
        if (!isActive)
            return;

        // Navigation
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            isRetrySelected = true;
            retryHighlighted.SetActive(true);
            quitHighlighted.SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            isRetrySelected = false;
            retryHighlighted.SetActive(false);
            quitHighlighted.SetActive(true);
        }

        // Enter
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            if (isRetrySelected)
                Retry();
            else
                Quit();
        }

        // Mouse (same as Main Menu)
        if (Input.GetMouseButtonDown(0))
        {
            if (Input.mousePosition.y > Screen.height / 2)
                Retry();
            else
                Quit();
        }
    }

    public void Show()
    {
        Debug.Log("SHOWING DEATH SCREEN");
        isActive = true;
        gameObject.SetActive(true);

        retryHighlighted.SetActive(true);
        quitHighlighted.SetActive(false);

        isRetrySelected = true;

        Time.timeScale = 0;

        PlayerController player = FindFirstObjectByType<PlayerController>();
        if (player != null)
            player.enabled = false;
    }

    public void Retry()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Quit()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }
}
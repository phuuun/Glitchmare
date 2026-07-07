using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScreen : MonoBehaviour
{
    [Header("Images")]
    public GameObject nextHighlighted;
    public GameObject quitHighlighted;

    [Header("Is this the final level?")]
    public bool finalLevel = false;

    private bool nextSelected = true;
    private bool isActive = false;

    void Start()
    {
        gameObject.SetActive(false);
    }

    void Update()
    {
        if (!isActive)
            return;

        // Final level only has Quit
        if (!finalLevel)
        {
            if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                nextSelected = true;
                UpdateImages();
            }

            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                nextSelected = false;
                UpdateImages();
            }
        }

        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            if (finalLevel)
            {
                QuitToMenu();
                return;
            }

            if (nextSelected)
                NextLevel();
            else
                QuitToMenu();
        }
    }

    void UpdateImages()
    {
        nextHighlighted.SetActive(nextSelected);
        quitHighlighted.SetActive(!nextSelected);
    }

    public void Show()
    {
        Time.timeScale = 0f;
        isActive = true;

        gameObject.SetActive(true);

        nextSelected = true;

        UpdateImages();
        PlayerController player = FindAnyObjectByType<PlayerController>();

        if (player != null)
            player.enabled = false;
    }

    void NextLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(2);
    }

    void QuitToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
}
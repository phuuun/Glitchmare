using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [Header("Images")]
    public GameObject playHighlighted;  // Drag the "Play Highlighted" image here
    public GameObject quitHighlighted;  // Drag the "Quit Highlighted" image here

    private bool isPlaySelected = true;

    void Start()
    {
        // Start with Play highlighted
        playHighlighted.SetActive(true);
        quitHighlighted.SetActive(false);
    }

    void Update()
    {
        // --- NAVIGATION (Up/Down Arrow Keys or W/S) ---
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            // Move UP = Select Play (top option)
            isPlaySelected = true;
            playHighlighted.SetActive(true);
            quitHighlighted.SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            // Move DOWN = Select Quit (bottom option)
            isPlaySelected = false;
            playHighlighted.SetActive(false);
            quitHighlighted.SetActive(true);
        }

        // --- SELECT (Enter Key) ---
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            if (isPlaySelected)
            {
                PlayGame();
            }
            else
            {
                QuitGame();
            }
        }

        // --- Mouse Click (Top half = Play, Bottom half = Quit) ---
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = Input.mousePosition;
            float height = Screen.height;
            
            if (mousePos.y > height / 2)
            {
                // Clicked top half = Play
                PlayGame();
            }
            else
            {
                // Clicked bottom half = Quit
                QuitGame();
            }
        }
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(1); // Load level scene (index 1 in Build Settings)
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}   
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player_MainMenu : MonoBehaviour
{
    [Header("Buttons")]
    public Button pauseButton;
    public Button resumeButton;
    public Button quitButton;

    private bool isPaused = false;

    private void Start()
    {
        if (pauseButton)
            pauseButton.onClick.AddListener(PauseGame);

        if (resumeButton)
            resumeButton.onClick.AddListener(ResumeGame);

        if (quitButton)
            quitButton.onClick.AddListener(QuitGame);
    }

    public void PauseGame()
    {
        if (!isPaused)
        {
            Time.timeScale = 0;
            isPaused = true;
        }
    }

    public void ResumeGame()
    {
        if (isPaused)
        {
            Time.timeScale = 1;
            isPaused = false;
            gameObject.SetActive(false);
        }
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }
}

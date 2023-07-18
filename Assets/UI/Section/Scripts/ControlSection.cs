using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlSection : MonoBehaviour
{
    public bool GamePaused { get { return gamePaused; } }
    public bool GameFinished { get { return gameFinished; } }

    bool gamePaused;
    bool gameFinished;

    public void StartGame()
    {
        ContinueGame();
        SceneManager.LoadScene(1);
    }

    public void CloseGame()
    {
        Application.Quit();
    }

    public void EndGame()
    {
        PauseGame();
        gameFinished = true;
    }

    public void PauseGame()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        Time.timeScale = 0;
        gamePaused = true;
    }

    public void ContinueGame()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1;
        gamePaused = false;
    }
}

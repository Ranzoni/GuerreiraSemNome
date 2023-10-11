using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlSection : MonoBehaviour
{
    public void StartGame()
    {
        StartCoroutine(StartGameRoutine());
    }

    public IEnumerator StartGameRoutine()
    {
        yield return new WaitForSecondsRealtime(1f);

        UnlockScreen();
        SceneManager.LoadScene(1);
    }

    public void FinishGame()
    {
        LockScreen();
        SceneManager.LoadScene(2);
    }

    public void CloseGame()
    {
        Application.Quit();
    }

    public void LockScreen()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        Time.timeScale = 0;
    }

    public void UnlockScreen()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1;
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlSection : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
}

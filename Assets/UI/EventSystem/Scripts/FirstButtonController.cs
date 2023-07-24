using UnityEngine;
using UnityEngine.UI;

public class FirstButtonController : MonoBehaviour
{
    [Tooltip("Botão inicial do menu de Pause do jogo")]
    [SerializeField] Button firstPauseButton;
    [Tooltip("Botão inicial do menu de Game Over do jogo")]
    [SerializeField] Button firstGameOuverButton;

    public void SelectPauseButton()
    {
        firstPauseButton.Select();
    }

    public void SelectGameOverButton()
    {
        firstGameOuverButton.Select();
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject mainPanel = null, instructionsPanel = null;

    private void Start() => MainMenu();
    public void Play() => SceneManager.LoadScene(SceneNames.Game);
    public void MainMenu() => EnableMenu(true, false);
    public void Instructions() => EnableMenu(false, true);
    public void Exit() => Application.Quit();

    private void EnableMenu(bool main, bool instructions)
    {
        mainPanel.SetActive(main);
        instructionsPanel.SetActive(instructions);
    }
}
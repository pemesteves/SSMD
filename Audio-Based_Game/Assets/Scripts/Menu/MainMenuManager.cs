using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject mainPanel = null, instructionsPanel = null, optionsPanel = null;

    private void Start() => MainMenu();
    public void Play() => SceneManager.LoadScene(SceneNames.Game);
    public void MainMenu() => EnableMenu(true, false, false);
    public void Instructions() => EnableMenu(false, true, false);
    public void Options() => EnableMenu(false, false, true);
    public void Exit() => Application.Quit();

    private void EnableMenu(bool main, bool instructions, bool options)
    {
        mainPanel.SetActive(main);
        instructionsPanel.SetActive(instructions);
        optionsPanel.SetActive(options);
    }

    public void OnAccessibilityToggleChanged(bool newValue) => UAP_AccessibilityManager.EnableAccessibility(newValue, true);
}
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject mainPanel = null, instructionsPanel = null, optionsPanel = null;

    [SerializeField] private Toggle optionsToggle = null;
    private const string AccessibilityKey = "accessibility";

    private void Start()
    {
        MainMenu();

        bool isOn = PlayerPrefs.GetInt(AccessibilityKey, 1) == 1;
        optionsToggle.isOn = isOn;
        OnAccessibilityToggleChanged(isOn);
    }

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

    public void OnAccessibilityToggleChanged(bool newValue)
    {
        UAP_AccessibilityManager.EnableAccessibility(newValue, true);
        PlayerPrefs.SetInt(AccessibilityKey, newValue ? 1 : 0);
        PlayerPrefs.Save();
    }
}
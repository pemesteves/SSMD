using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager instance;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
    #endregion

    [SerializeField] private GameObject gameOverPanel = null;

    private void Start() => GameOver(false);
    public void PlayAgain() => SceneManager.LoadScene(SceneNames.Game);
    public void MainMenu() => SceneManager.LoadScene(SceneNames.Menu);
    public void GameOver(bool active = true) => gameOverPanel.SetActive(active);
}
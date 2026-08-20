using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public bool IsGameStarted { get; private set; }
    public bool IsGameOver { get; private set; }

    [SerializeField] private DoofusController doofus;
    [SerializeField] private float fallThreshold = -10f;
    [SerializeField] private GameObject startScreen;
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private TMP_Text finalScoreText;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Update()
    {
        if (!IsGameStarted)
            return;

        if (IsGameOver)
            return;

        if (doofus == null)
            return;

        if (doofus.transform.position.y <= fallThreshold)
        {
            GameOver();
        }
    }

    public void StartGame()
    {
        if (IsGameOver)
            return;

        IsGameStarted = true;

        if (startScreen != null)
        {
            startScreen.SetActive(false);
        }

        Debug.Log("GAME STARTED.");
    }

    private void GameOver()
    {
        IsGameOver = true;

        if (finalScoreText != null &&
            ScoreManager.Instance != null)
        {
            finalScoreText.text =
                "Score: " + ScoreManager.Instance.Score;
        }

        if (gameOverScreen != null)
        {
            gameOverScreen.SetActive(true);
        }

        Debug.Log("GAME OVER - Doofus fell.");
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(
            SceneManager.GetActiveScene().name
        );
    }
}
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    public int Score { get; private set; }

    [SerializeField] private TMP_Text scoreText;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        Score = 0;

        UpdateScoreUI();
    }

    public void AddPoint()
    {
        if (GameManager.Instance != null &&
            GameManager.Instance.IsGameOver)
        {
            return;
        }

        Score++;

        Debug.Log("Score: " + Score);

        UpdateScoreUI();
    }

    public void ResetScore()
    {
        Score = 0;

        UpdateScoreUI();
    }

    private void UpdateScoreUI()
    {
        if (scoreText == null)
            return;

        scoreText.text = "Score: " + Score;
    }
}